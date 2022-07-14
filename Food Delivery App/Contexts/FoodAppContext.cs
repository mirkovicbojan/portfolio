using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Delivery_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Delivery_App.Contexts
{
    public class FoodAppContext : DbContext
    {
        public DbSet<User> users { get; set; }

        public DbSet<Restaurant> restaurants { get; set; }

        public DbSet<Food> foods { get; set; }

        public FoodAppContext(DbContextOptions<FoodAppContext> options):base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlite(configuration.GetConnectionString("Default Connection"));
            }
        } 
    }
}