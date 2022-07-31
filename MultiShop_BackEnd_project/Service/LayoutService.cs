using Microsoft.AspNetCore.Http;
using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
using MultiShop_BackEnd_project.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Service
{
    public class LayoutService
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor http;

        public LayoutService(AppDbContext context,IHttpContextAccessor http)
        {
            this.context = context;
            this.http = http;
        }
        public List<Setting> GetSettings()
        {
            List<Setting> settings = context.Settings.ToList();
            return settings;
        }

        public BasketVM GetBasket()
        {
            //BasketVM basket = new BasketVM();

            string basketStr = http.HttpContext.Request.Cookies["Basket"];
            if (!string.IsNullOrEmpty(basketStr))
            {
               BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                foreach(BasketCookieItemVM cookie in basket.BasketCookieItemVMs)
                {
                    Clothes existed = context.Clothes.FirstOrDefault(c => c.Id == cookie.Id);
                    if (existed == null)
                    {
                        basket.BasketCookieItemVMs.Remove(cookie);
                    }
                }
                return basket;
            }
            return null;


        }
    }
}
