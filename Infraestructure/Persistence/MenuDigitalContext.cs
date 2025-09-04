using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence
{
    internal class MenuDigitalContext : DbContext
    {
        public DbSet<Dish> Dish { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<DeliveryType> DeliveryType { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Dish Configuration
            modelBuilder.Entity<Dish>()
                .ToTable("Dish")
                .HasOne<Category>(d => d.CategoryDb)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.Category);

            modelBuilder.Entity<Dish>()
                .HasMany<OrderItem>(d => d.OrderItems)
                .WithOne(oi => oi.DishDb)
                .HasForeignKey(oi => oi.Dish);

            // Category Configuration
            modelBuilder.Entity<Category>()
                .ToTable("Category")
                .HasMany<Dish>(c => c.Dishes)
                .WithOne(d => d.CategoryDb)
                .HasForeignKey(d => d.Category);

            // Order Configuration
            modelBuilder.Entity<Order>()
                .ToTable("Order")
                .HasMany<OrderItem>(o => o.OrderItems)
                .WithOne(oi => oi.OrderDb)
                .HasForeignKey(oi => oi.Order);

            modelBuilder.Entity<Order>()
                .HasOne<DeliveryType>(o => o.DeliveryTypeDb)
                .WithMany(dt => dt.Orders)
                .HasForeignKey(o => o.DeliveryType);

            modelBuilder.Entity<Order>()
                .HasOne<Status>(o => o.StatusDb)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.OverallStatus);

            // OrderItem Configuration
            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItem")
                .HasOne<Dish>(oi => oi.DishDb)
                .WithMany(d => d.OrderItems)
                .HasForeignKey(oi => oi.Dish);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>(oi => oi.OrderDb)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.Order);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Status>(oi => oi.StatusDb)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(oi => oi.Status);

            // DeliveryType Configuration
            modelBuilder.Entity<DeliveryType>()
                .ToTable("DeliveryType")
                .HasMany<Order>(dt => dt.Orders)
                .WithOne(o => o.DeliveryTypeDb)
                .HasForeignKey(o => o.DeliveryType);

            // Status Configuration
            modelBuilder.Entity<Status>()
                .ToTable("Status")
                .HasMany<Order>(s => s.Orders)
                .WithOne(o => o.StatusDb)
                .HasForeignKey(o => o.OverallStatus);

            modelBuilder.Entity<Status>()
                .HasMany<OrderItem>(s => s.OrderItems)
                .WithOne(oi => oi.StatusDb)
                .HasForeignKey(oi => oi.Status);
        }
    }
}
