using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiShop_BackEnd_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop_BackEnd_project.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Ads> Ads { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<ClothesInfo> ClothesInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClothesImage> ClothesImages { get; set; }
        public DbSet<Setting> Settings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                )
            {
                item.SetColumnType("decimal(6,2)");
                //item.SetDefaultValue(20.5m); default deyer yazilisi bele verilri
            }

            modelBuilder.Entity<Setting>()
                .HasIndex(c => c.Key)
                .IsUnique();
            base.OnModelCreating(modelBuilder);





        }
    }


    

}


