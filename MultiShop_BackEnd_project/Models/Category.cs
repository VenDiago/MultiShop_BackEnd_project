using Microsoft.AspNetCore.Http;
using MultiShop_BackEnd_project.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShop_BackEnd_project.Models
{
    public class Category:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Clothes> Clothes { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
