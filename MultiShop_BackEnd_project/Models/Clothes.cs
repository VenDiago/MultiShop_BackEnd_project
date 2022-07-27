using MultiShop_BackEnd_project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Models
{
    public class Clothes:BaseEntity
    {
       
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Desc { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Tags { get; set; }

        public int? Quantity { get; set; }
        public ClothesInfo ClothesInfo{ get; set; }
        public int ClothesInfoId { get; set; }
        public List<ClothesImage> ClothesImages { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
