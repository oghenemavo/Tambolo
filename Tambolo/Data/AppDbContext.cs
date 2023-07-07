﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Tambolo.Models;

namespace Tambolo.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);

            builder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired();

            builder.Entity<Product>()
                .HasMany(c => c.Orders)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId)
                .IsRequired();
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name = "Super Admin",
                    NormalizedName = "SUPER ADMIN",
                    ConcurrencyStamp = "1",
                },
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "2",
                },
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "3",
                },
                new IdentityRole()
                {
                    Name = "Vendor",
                    NormalizedName = "VENDOR",
                    ConcurrencyStamp = "4",
                }
            );
        }
    }
}
