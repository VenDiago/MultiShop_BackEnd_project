using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
using MultiShop_BackEnd_project.ViewModels;
using Newtonsoft.Json;
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

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Clothes clothes = await context.Clothes.FirstOrDefaultAsync(c => c.Id == id);
            if (clothes == null) return NotFound();
            string basketStr = HttpContext.Request.Cookies["Basket"];
            BasketVM basket;

            if (string.IsNullOrEmpty(basketStr))
            {
                basket = new BasketVM();
                BasketCookieItemVM cookieItem = new BasketCookieItemVM
                {
                    Id = clothes.Id,
                    Quantity = 1

                };
                basket.BasketCookieItemVMs = new List<BasketCookieItemVM>();
                basket.BasketCookieItemVMs.Add(cookieItem);
                basket.TotalPrice = clothes.Price;

            }
            else
            {
                basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                BasketCookieItemVM existed = basket.BasketCookieItemVMs.Find(c => c.Id == id);
                if (existed == null)
                {
                    BasketCookieItemVM cookieItem = new BasketCookieItemVM
                    {
                        Id = clothes.Id,
                        Quantity = 1

                    };
                    basket.BasketCookieItemVMs.Add(cookieItem);
                    basket.TotalPrice += clothes.Price;
                }
                else
                {
                    basket.TotalPrice += clothes.Price;
                    existed.Quantity++;
                }
            }




            basketStr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", basketStr);


            //int ID = 1;
            //return RedirectToAction("Detail", new { id = ID });
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowBasket()
        {
            if (HttpContext.Request.Cookies["Basket"] == null) return NotFound();
            BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(HttpContext.Request.Cookies["Basket"]);
            return Json(basket);
        }
    }
}
