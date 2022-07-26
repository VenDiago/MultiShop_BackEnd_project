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
    public class Slider:BaseEntity
    {
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public string ButtonUrl { get; set; }
        [Required]
        public byte Order { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
