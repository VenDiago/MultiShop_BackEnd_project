using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
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
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, Ads newAds)
        {
            if (id is null || id == 0) return NotFound();
            if (!ModelState.IsValid) return View();
            Ads existed = context.Ads.FirstOrDefault(s => s.Id == id);
            if (existed == null) return NotFound();
            bool duplicate = context.Ads.Any(a => a.Image == newAds.Image);
            if (duplicate)
            {
                ModelState.AddModelError("Photo", "You cannot duplicate photo");
                return View();
            }

            context.Entry(existed).CurrentValues.SetValues(newAds);
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
