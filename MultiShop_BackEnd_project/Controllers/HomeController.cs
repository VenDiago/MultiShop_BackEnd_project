using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
using MultiShop_BackEnd_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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

        //public List<Clothes> GetClothesList()
        //{
        //    var clothes = context.Clothes.OrderByDescending(x => x.Id).ToList();
        //    return clothes;
        //}

        //public PartialViewResult ClothesListPartial(int? page,int? category)
        //{
        //    var pageNumber = page ?? 1;
        //    var pageSize = 10;
        //    if (category != null)
        //    {
        //        ViewBag.category = category;
        //        var clothesList = context.Clothes
        //            .OrderByDescending(x => x.Id)
        //            .Where(x => x.CategoryId == category)
        //            .ToPagedList(pageNumber, pageSize);
        //        return PartialView(clothesList);
                    
        //    }
        //    else
        //    {
        //        var clothesList = context.Clothes.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);
        //        return PartialView(clothesList);
        //    }
        //}

        public ViewResult  List(string category)
        {
            string _category = category;
            IEnumerable<Clothes> clothes;

            string currentCategory = string.Empty;
            if (string.IsNullOrEmpty(category))
            {
                clothes = context.Clothes.OrderBy(c => c.Id);
                currentCategory = "All clothes";
            }
            else
            {
                if (string.Equals("Clothes", _category, StringComparison.OrdinalIgnoreCase))
                {
                    clothes = context.Clothes.Where(c => c.Category.Name.Equals("Clothes"));

                }
                else
                {
                    clothes = context.Clothes.Where(c => c.Category.Name.Equals("Non-Clothes"));

                    currentCategory = _category;
                }
                
            }
            var clothesListVM = new ClothesListVM
            {
                Clothes = clothes.ToList(),
                //Category = category.ToList()
            };
            return View(clothesListVM);
        }
       


    }
}
