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

        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, Slider newSlider)
        {
            if (id is null || id == 0) return NotFound();
            if (!ModelState.IsValid) return View();
            Slider existed = context.Sliders.FirstOrDefault(s => s.Id == id);
            if (existed == null) return NotFound();
            bool duplicate = context.Sliders.Any(s => s.Image == newSlider.Image);
            if (duplicate)
            {
                ModelState.AddModelError("Photo", "You cannot duplicate photo");
                return View();
            }

            context.Entry(existed).CurrentValues.SetValues(newSlider);
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
    }
}
