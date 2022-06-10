using Microsoft.EntityFrameworkCore;
using TimeSheet.Models;

namespace TimeSheet.Contexts
{
    public class TimeSheetContext : DbContext
    {
        public TimeSheetContext(DbContextOptions<TimeSheetContext> options) : base(options) { }

        public DbSet<TimeSheetClass> TimeSheets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Client> Client { get; set; }


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
                    .UseSqlServer(configuration.GetConnectionString("Default Connection"));
            }

        }

    }
}