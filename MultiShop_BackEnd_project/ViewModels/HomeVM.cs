using MultiShop_BackEnd_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.ViewModels
{
    public class HomeVM
    {
        public List<Slider>Sliders{ get; set; }
        public List<Ads>Ads { get; set; }
    }
}
