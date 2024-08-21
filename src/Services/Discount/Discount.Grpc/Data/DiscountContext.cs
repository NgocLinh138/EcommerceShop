using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "Basic T-Shirt", Description = "Discount 50", Amount = 50 },
                new Coupon { Id = 2, ProductName = "Denim Jeans", Description = "Discount 100.", Amount = 100 }
                );
        }
    }
}
