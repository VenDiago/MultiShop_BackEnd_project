using Microsoft.AspNetCore.Mvc;
using MultiShop_BackEnd_project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext context;

        public ProductController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Detail(int? id)
        {
            //if (id is null || id == 0) return NotFound();
            return View();
        }
    }
}
