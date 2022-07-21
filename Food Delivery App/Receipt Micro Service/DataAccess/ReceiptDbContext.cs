using Microsoft.EntityFrameworkCore;
using Receipt_Micro_Service.Models;

namespace Receipt_Micro_Service.DataAccess
{
    public class ReceiptDbContext : DbContext
    {
        public ReceiptDbContext() { }

        public ReceiptDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Receipt> Receipts {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SnakeCaseIdentityTableNames(modelBuilder);
        }

        private static void SnakeCaseIdentityTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receipt>(b => {b.ToTable("receipts");});
        }
    }
}