using Microsoft.AspNetCore.Mvc;
using MultiShop_BackEnd_project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Areas.MultiShopAdmin.Controllers
{
    [Area("MultiShopAdmin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext context;

        public DashboardController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
