using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence
{
    public class MenuDigitalContext : DbContext
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
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish");

                entity.HasKey(d => d.DishId);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(255);
                entity.Property(d => d.Description).IsRequired().HasColumnType("text");
                entity.Property(d => d.Available).HasDefaultValue(true);
                entity.Property(d => d.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(d => d.ImageUrl).IsRequired().HasColumnType("text");
                entity.Property(d => d.CreateDate)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(d => d.UpdateDate)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<Category>(d => d.CategoryDb)
                      .WithMany(c => c.Dishes)
                      .HasForeignKey(d => d.Category);
                entity.HasMany<OrderItem>(d => d.OrderItems)
                      .WithOne(oi => oi.DishDb)
                      .HasForeignKey(oi => oi.Dish);
            });

            // Category Configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(25);
                entity.Property(c => c.Description).IsRequired().HasMaxLength(255);
                entity.Property(c => c.Order).IsRequired().ValueGeneratedOnAdd();
            });

            // Order Configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.OrderId).HasColumnType("bigint").ValueGeneratedOnAdd();
                entity.Property(o => o.DeliveryTo).IsRequired().HasMaxLength(255);
                entity.Property(o => o.Notes).HasColumnType("text");
                entity.Property(o => o.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(o => o.CreateDate)
                      .HasColumnType("timestamp")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(o => o.UpdateDate)
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasMany<OrderItem>(o => o.OrderItems)
                      .WithOne(oi => oi.OrderDb)
                      .HasForeignKey(oi => oi.Order);

                entity.HasOne<DeliveryType>(o => o.DeliveryTypeDb)
                        .WithMany(dt => dt.Orders)
                        .HasForeignKey(o => o.DeliveryType);

                entity.HasOne<Status>(o => o.StatusDb)
                        .WithMany(s => s.Orders)
                        .HasForeignKey(o => o.OverallStatus);
            });

            // OrderItem Configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.HasKey(oi => oi.OrderItemId);
                entity.Property(oi => oi.OrderItemId).HasColumnType("bigint").ValueGeneratedOnAdd();
                entity.Property(oi => oi.Quantity).IsRequired();
                entity.Property(oi => oi.Notes).HasColumnType("text");
                entity.Property(oi => oi.CreateDate)
                      .HasColumnType("timestamp")
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<Dish>(oi => oi.DishDb)
                        .WithMany(d => d.OrderItems)
                        .HasForeignKey(oi => oi.Dish);

                entity.HasOne<Order>(oi => oi.OrderDb)
                        .WithMany(o => o.OrderItems)
                        .HasForeignKey(oi => oi.Order);

                entity.HasOne<Status>(oi => oi.StatusDb)
                        .WithMany(s => s.OrderItems)
                        .HasForeignKey(oi => oi.Status);
            });

            // DeliveryType Configuration
            modelBuilder.Entity<DeliveryType>(entity =>
            {
                entity.ToTable("DeliveryType");

                entity.HasKey(dt => dt.Id);
                entity.Property(dt => dt.Id).ValueGeneratedOnAdd();
                entity.Property(dt => dt.Name).IsRequired().HasMaxLength(25);
            });

            // Status Configuration
            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd();
                entity.Property(s => s.Name).IsRequired().HasMaxLength(25);
            });
        }
    }
}
