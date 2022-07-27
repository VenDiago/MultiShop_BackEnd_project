﻿using MultiShop_BackEnd_project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Clothes> Clothes { get; set; }
    }
}