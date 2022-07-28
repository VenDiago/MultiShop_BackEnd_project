using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
using MultiShop_BackEnd_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            HomeVM model = new HomeVM
            {
                Sliders = context.Sliders.ToList(),
                Ads = context.Ads.ToList(),
                Clothes=context.Clothes.Include(c=>c.ClothesImages).ToList(),
                Category=context.Categories.Include(c=>c.Clothes).ToList()
            };
            return View(model);
        }
     
    }
}
