using MultiShop_BackEnd_project.DAL;
using MultiShop_BackEnd_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.Service
{
    public class LayoutService
    {
        private readonly AppDbContext context;

        public LayoutService(AppDbContext context)
        {
            this.context = context;
        }
        public List<Setting> GetSettings()
        {
            List<Setting> settings = context.Settings.ToList();
            return settings;
        }
    }
}
