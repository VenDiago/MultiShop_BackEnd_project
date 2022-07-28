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
    public class CategoryController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public IActionResult Index()
        {
            List<Category> model = context.Categories.ToList();
            return View(model);
        }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            if (category.Photo is null)
            {
                ModelState.AddModelError("Photo", "You have to choose 1 image at least");
                return View();
            }
            if (!category.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Please choose image file");
                return View();
            }
            if (category.Photo.Length / 1024 / 1024 > 2)
            {
                ModelState.AddModelError("Photo", "Image size 2MB");
                return View();
            };

            string filename = string.Concat(Guid.NewGuid(), category.Photo.FileName);
            string path = Path.Combine(env.WebRootPath, env.WebRootPath, "assets", "img");
            string filepath = Path.Combine(path, filename);

            try
            {
                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    await category.Photo.CopyToAsync(stream);

                }
            }
            catch (Exception)
            {
                throw new FileLoadException();
            }
            category.Image = filename;

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, Category newCategory)
        {
            if (id is null || id == 0) return NotFound();
            if (!ModelState.IsValid) return View();
            Category existed = context.Categories.FirstOrDefault(c => c.Id == id);
            if (existed == null) return NotFound();
            bool duplicate = context.Categories.Any(c => c.Image == newCategory.Image); 
            if (duplicate)
            {
                ModelState.AddModelError("Photo", "You cannot duplicate photo");
                return View();
            }

            context.Entry(existed).CurrentValues.SetValues(newCategory);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Category category = await context.Categories.FindAsync(id);
            if (category is null) return NotFound();
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail()
        {
            return View();
        }

    }
}
