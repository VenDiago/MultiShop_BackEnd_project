using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
using MultiShop_BackEnd_project.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext context;

        public ShopController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int? page)
        {
            HomeVM model = new HomeVM
            {
                Clothes = context.Clothes.Include(c => c.ClothesImages).ToList(),
            };
            return View(model);


            List<Clothes> clothesList = new List<Clothes>();
            clothesList = context.Clothes.ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(clothesList.ToPagedList(pageNumber, pageSize));

        }

        public IActionResult ShopCart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

    }
}
