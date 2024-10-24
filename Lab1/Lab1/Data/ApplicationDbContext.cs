using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Lab1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "1 name" },
                new Product { Id = 2, Name = "2 name" },
                new Product { Id = 3, Name = "3 name" },
                new Product { Id = 4, Name = "4 name" },
                new Product { Id = 5, Name = "5 name" },
                new Product { Id = 6, Name = "6 name" },
                new Product { Id = 7, Name = "7 name" }
            );
        }
    }
}
