using MultiShop_BackEnd_project.Models;
using System;
using System.Collections.Generic;

namespace MultiShop_BackEnd_project.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Ads> Ads { get; set; }
        public List<Clothes> Clothes { get; set; }
        public List<Category> Category { get; set; }

    }
}
