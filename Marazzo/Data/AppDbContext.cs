using Marazzo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Spec> Specs { get; set; }
        public DbSet<SpecDetail> SpecDetails { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<PayMethod> PayMethods { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Setting> Settings {get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Banner> Banners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<Brand>()
                .HasMany(c => c.Products)
                .WithOne(s => s.Brand)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .HasMany(a => a.SpecDetails)
                .WithOne(b => b.Product)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Spec>()
                .HasMany(a => a.SpecDetails)
                .WithOne(b => b.Spec)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Subcategory>()
                .HasMany(a => a.Products)
                .WithOne(b => b.Subcategory)
                .OnDelete(DeleteBehavior.Cascade);
        }

       

       


}
}
