using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
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
            if (!ModelState.IsValid) return View();
            await context.Clothes.AddAsync(clothes);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
