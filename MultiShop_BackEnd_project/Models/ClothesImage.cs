using MultiShop_BackEnd_project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Models
{
    public class ClothesImage:BaseEntity
    {
        public string Name { get; set; }
        public string Alternative { get; set; }
        public bool? IsMain { get; set; }
        public int ClothesId { get; set; }
        public Clothes Clothes { get; set; }
    }
}
