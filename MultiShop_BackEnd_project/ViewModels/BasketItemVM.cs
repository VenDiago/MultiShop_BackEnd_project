using MultiShop_BackEnd_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.ViewModels
{
    public class BasketItemVM
    {
        public Clothes clothes { get; set; }
        public int Quantity { get; set; }
    }
}
