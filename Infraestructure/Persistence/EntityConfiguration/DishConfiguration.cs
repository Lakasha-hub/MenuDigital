using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.builderConfiguration
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dish");

            builder.HasKey(d => d.DishId);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(255);
            builder.Property(d => d.Description).IsRequired().HasColumnType("text");
            builder.Property(d => d.Available).HasDefaultValue(true);
            builder.Property(d => d.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(d => d.ImageUrl).IsRequired().HasColumnType("text");
            builder.Property(d => d.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
            builder.Property(d => d.UpdateDate)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.HasOne<Category>(d => d.CategoryDb)
                  .WithMany(c => c.Dishes)
                  .HasForeignKey(d => d.Category);

            builder.HasMany<OrderItem>(d => d.OrderItems)
                  .WithOne(oi => oi.DishDb)
                  .HasForeignKey(oi => oi.Dish);
        }
    }
}
