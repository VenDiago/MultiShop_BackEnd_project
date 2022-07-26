﻿using MultiShop_BackEnd_project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Models
{
    public class Ads:BaseEntity
    {
        public string Image { get; set; }
        public  string Discount { get; set; }
        public string Title { get; set; }
        public string ButtonUrl { get; set; }
        public byte Order { get; set; }
    }
}