using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Uplift.Models;

namespace UpliftUdemy.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<OrderHeader> OrderHeaders{ get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<WebImages> WebImages { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .Property<string>("Name").HasColumnName("Category Name").IsRequired();
            builder.Entity<Category>()
               .Property<int>("DisplayOrder").HasColumnName("Display Order").IsRequired();


            builder.Entity<Service>()
               .Property<string>("Name").HasColumnName("Service Name").IsRequired();
            builder.Entity<Service>()
            .Property<string>("LongDescription").HasColumnName("Description");
            builder.Entity<Service>()
            .Property<string>("ImageUrl").HasColumnName("Image");

            builder.Entity<OrderHeader>()
                .Property<string>("Name").IsRequired();
            
            builder.Entity<OrderHeader>()
                .Property<string>("Email").IsRequired();
            builder.Entity<OrderHeader>()
                .Property<string>("Address").IsRequired();
            builder.Entity<OrderHeader>()
                .Property<DateTime>("OrderDate").HasColumnType("date");

            builder.Entity<ApplicationUser>()
               .Property<string>("Name").IsRequired();

            builder.Entity<WebImages>()
             .Property<string>("Name").IsRequired();
         
        }

    }
}
