﻿using MultiShop_BackEnd_project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Models
{
    public class ClothesInfo:BaseEntity
    {
        public string AdditionaInformation { get; set; }
        public string ProductDescription { get; set; }
        public List<Clothes> Clothes { get; set; }

    }
}
