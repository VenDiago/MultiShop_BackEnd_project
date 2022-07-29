using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Controllers
{
    public class ClothesController : Controller
    {
        private readonly AppDbContext context;

        public ClothesController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Clothes clothes = await context.Clothes
                .Include(c => c.ClothesImages)
                .Include(c => c.ClothesInfo)
                .Include(c => c.Category)
                .FirstOrDefaultAsync();
            ViewBag.Clothes = await context.Clothes.Include(c => c.ClothesImages).ToListAsync();
            if (clothes is null) return NotFound();
            return View(clothes);
        }
    }
}
