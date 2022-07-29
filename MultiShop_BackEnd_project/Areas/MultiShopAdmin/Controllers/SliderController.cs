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
    public class SliderController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public IActionResult Index()
        {
            List<Slider> model = context.Sliders.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if (slider.Photo is null)
            {
                ModelState.AddModelError("Photo", "You have to choose 1 image at least");
                return View();
            }
            if (!slider.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please choose image file");
                return View();
            }
            if (slider.Photo.Length / 1024 / 1024 > 2)
            {
                ModelState.AddModelError("Photo", "Image size 2MB");
                return View();
            };

            string filename = string.Concat(Guid.NewGuid(), slider.Photo.FileName);
            string path = Path.Combine(env.WebRootPath, env.WebRootPath, "assets","img");
            string filepath = Path.Combine(path, filename);

            try
            {
                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    await slider.Photo.CopyToAsync(stream);

                }
            }
            catch (Exception)
            {
                throw new FileLoadException();

            }
            slider.Image = filename;

            await context.Sliders.AddAsync(slider);
            await context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Slider slider = context.Sliders.FirstOrDefault(c => c.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, Slider newSlider)
        {
            if (id is null || id == 0) return NotFound();


            Slider existed = context.Sliders.FirstOrDefault(s => s.Id == id);
            if (!ModelState.IsValid) return View(existed);
            if (existed == null) return NotFound();
            bool duplicate = context.Categories.Any(s => s.Id != existed.Id && s.Name == newSlider.Title);
            if (duplicate)
            {
                ModelState.AddModelError("Name", "You cannot duplicate title");
                return View();
            }

            //existed.Name = newCategory.Name;
            context.Entry(existed).CurrentValues.SetValues(newSlider);
            if (newSlider.Photo != null)
            {
                if (!newSlider.Photo.ImageIsOkay(2))
                {
                    ModelState.AddModelError("Image", "You cannot duplicate image");
                    return View();
                }
                if (existed.Image != null)
                    FileValidator.FileDelete(env.WebRootPath, "assets/img", existed.Image);
                existed.Image = await FileValidator.FileCreate(newSlider.Photo, env.WebRootPath, "assets/img");
            }
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Slider slider = await context.Sliders.FindAsync(id);
            if (slider is null) return NotFound();
            context.Sliders.Remove(slider);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id == 0) return NotFound();
            //Slider slider = await context.Sliders.FindAsync(id);
            Slider existed = context.Sliders.FirstOrDefault(s => s.Id == id);
            if (existed is null) return NotFound();
            return View(existed);
            
        }
    }
}
