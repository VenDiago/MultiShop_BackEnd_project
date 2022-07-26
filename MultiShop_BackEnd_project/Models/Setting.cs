using MultiShop_BackEnd_project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Models
{
    public class Setting:BaseEntity

    {
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
