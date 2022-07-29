using Microsoft.AspNetCore.Http;
using MultiShop_BackEnd_project.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Models
{
    public class Clothes:BaseEntity
    {
       [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Tags { get; set; }

        [Required]
        public int? Quantity { get; set; }
        public ClothesInfo ClothesInfo{ get; set; }
        public int ClothesInfoId { get; set; }
        public List<ClothesImage> ClothesImages { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [NotMapped]
        public List<IFormFile> Photos { get; set; }

        [NotMapped]
        public IFormFile MainPhoto { get; set; }
        [NotMapped]
        public List<int>ImagesId { get; set; }


    }
}
