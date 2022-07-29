using Microsoft.AspNetCore.Mvc;
using MultiShop_BackEnd_project.DAL;
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
        public IActionResult Index()
        {
            return View();
        }

    }
}
