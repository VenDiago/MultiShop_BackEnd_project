using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
using MultiShop_BackEnd_project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Areas.MultiShopAdmin.Controllers
{
    [Area("MultiShopAdmin")]
    public class ClothesController : Controller
    {

        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;

        public ClothesController(AppDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public IActionResult Index()
        {
            List<Clothes> model = context.Clothes.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Information = context.ClothesInfos.ToList();
            ViewBag.Category = context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Clothes clothes)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Information = context.ClothesInfos.ToList();
                ViewBag.Categories = context.Categories.ToList();
                return View();
            }
            if (clothes.MainPhoto == null ||  clothes.Photos == null)
            {

                ViewBag.Information = context.ClothesInfos.ToList();
                ViewBag.Categories = context.Categories.ToList();
                ModelState.AddModelError(string.Empty, "You must choose 1 main photo");
                return View();
            }

            if (!clothes.MainPhoto.ImageIsOkay(2) )
            {
                ViewBag.Information = context.ClothesInfos.ToList();
                ViewBag.Categories = context.Categories.ToList();
                ModelState.AddModelError(string.Empty, "Please choose valid image file");
                return View();
            }

            clothes.ClothesImages = new List<ClothesImage>();
            TempData["FileName"] = "";
            List<IFormFile> removeable = new List<IFormFile>();
            foreach (var photo in clothes.Photos.ToList())
            {
                if (!photo.ImageIsOkay(2))
                {
                    removeable.Add(photo);
                    TempData["FileName"] += photo.FileName + ",";
                    continue;
                }
                ClothesImage another = new ClothesImage
                {
                    Name = await photo.FileCreate(env.WebRootPath, "assets/img/"),
                    IsMain = false,
                    Alternative = photo.Name,
                    Clothes = clothes
                };
                clothes.ClothesImages.Add(another);
            }

            clothes.Photos.RemoveAll(p => removeable.Any(r => r.FileName == p.FileName));

            ClothesImage main = new ClothesImage
            {
                Name = await clothes.MainPhoto.FileCreate(env.WebRootPath, "assets/img"),
                IsMain = true,
                Alternative = clothes.Name,
                Clothes = clothes
            };

            clothes.ClothesImages.Add(main);
            await context.Clothes.AddAsync(clothes);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Information = context.ClothesInfos.ToList();
            ViewBag.Categories = context.Categories.ToList();
            if (id is null || id == 0) return NotFound();
            Clothes clothes = await context.Clothes
                .Include(c => c.ClothesImages)
                .Include(c => c.ClothesInfo)
                .Include(c => c.Category)
                .ThenInclude(c => c.Clothes).SingleOrDefaultAsync(c => c.Id == id);
            if (clothes == null) return NotFound();
            return View(clothes);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Update")]
        public async Task<IActionResult> Edit(int? id, Clothes clothes)
        {
            if (id is null || id == 0) return NotFound();
            Clothes existed = await context.Clothes
                .Include(c => c.ClothesImages)
                .Include(c => c.ClothesInfo)
                .Include(c => c.Category)
                .ThenInclude(c => c.Clothes).SingleOrDefaultAsync(p => p.Id == id);
            if (existed == null) return NotFound();

            List<ClothesImage> removeable = existed.ClothesImages.Where(c => c.IsMain == false && !clothes.ImagesId.Contains(c.Id)).ToList();
            existed.ClothesImages.RemoveAll(p => removeable.Any(c => c.Id ==c.Id));

            return View(existed.ClothesImages);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Clothes clothes = await context.Clothes.FindAsync(id);
            if (clothes is null) return NotFound();
            context.Clothes.Remove(clothes);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Clothes existed = context.Clothes.FirstOrDefault(c => c.Id == id);
            if (existed is null) return NotFound();
            return View(existed);
        }
    }
}
