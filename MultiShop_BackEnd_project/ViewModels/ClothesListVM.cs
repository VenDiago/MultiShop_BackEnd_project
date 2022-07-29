using MultiShop_BackEnd_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.ViewModels
{
    public class ClothesListVM
    {
        public List<Clothes> Clothes { get; set; }
        public List<Category> Category { get; set; }
    }
}
