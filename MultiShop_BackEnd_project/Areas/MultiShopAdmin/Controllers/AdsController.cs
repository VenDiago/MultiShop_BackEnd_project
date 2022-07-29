using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
using MultiShop_BackEnd_project.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Areas.MultiShopAdmin.Controllers
{
    [Area("MultiShopAdmin")]
    public class AdsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;

        public AdsController(AppDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public IActionResult Index()
        {
            List<Ads> model = context.Ads.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Ads ads)
        {
            if (!ModelState.IsValid) return View();
            if (ads.Photo is null)
            {
                ModelState.AddModelError("Photo", "You have to choose 1 image at least");
                return View();
            }
            if (!ads.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please choose image file");
                return View();
            }
            if (ads.Photo.Length / 1024 / 1024 > 2)
            {
                ModelState.AddModelError("Photo", "Image size 2MB");
                return View();
            };

            string filename = string.Concat(Guid.NewGuid(), ads.Photo.FileName);
            string path = Path.Combine(env.WebRootPath, env.WebRootPath, "assets", "img");
            string filepath = Path.Combine(path, filename);

            try
            {
                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    await ads.Photo.CopyToAsync(stream);

                }
            }
            catch (Exception)
            {
                throw new FileLoadException();

            }
             ads.Image = filename;

            await context.Ads.AddAsync(ads);
            await context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Ads ads = context.Ads.FirstOrDefault(c => c.Id == id);
            if (ads == null) return NotFound();
            return View(ads);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, Ads newAds)
        {
            if (id is null || id == 0) return NotFound();


            Ads existed = context.Ads.FirstOrDefault(c => c.Id == id);
            if (!ModelState.IsValid) return View(existed);
            if (existed == null) return NotFound();
            bool duplicate = context.Ads.Any(a => a.Id != existed.Id && a.Title == newAds.Title);
            if (duplicate)
            {
                ModelState.AddModelError("Name", "You cannot duplicate name");
                return View();
            }

            //existed.Name = newCategory.Name;
            context.Entry(existed).CurrentValues.SetValues(newAds);
            if (newAds.Photo != null)
            {
                if (!newAds.Photo.ImageIsOkay(2))
                {
                    ModelState.AddModelError("Image", "You cannot duplicate image");
                    return View();
                }
                if (existed.Image != null)
                    FileValidator.FileDelete(env.WebRootPath, "assets/img", existed.Image);
                existed.Image = await FileValidator.FileCreate(newAds.Photo, env.WebRootPath, "assets/img");
            }
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Ads ads = await context.Ads.FindAsync(id);
            if (ads is null) return NotFound();
            context.Ads.Remove(ads);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Ads existed = context.Ads.FirstOrDefault(a => a.Id == id);
            if (existed is null) return NotFound();
            return View(existed);

        }
    }
}
