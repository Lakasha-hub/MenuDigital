using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.EntityConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(oi => oi.OrderItemId);
            builder.Property(oi => oi.OrderItemId).HasColumnType("bigint").ValueGeneratedOnAdd();
            builder.Property(oi => oi.Quantity).IsRequired();
            builder.Property(oi => oi.Notes).HasColumnType("text");
            builder.Property(oi => oi.CreateDate)
                  .HasColumnType("timestamp without time zone")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .IsRequired();

            builder.HasOne<Dish>(oi => oi.DishDb)
                    .WithMany(d => d.OrderItems)
                    .HasForeignKey(oi => oi.Dish);

            builder.HasOne<Order>(oi => oi.OrderDb)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.Order);

            builder.HasOne<Status>(oi => oi.StatusDb)
                    .WithMany(s => s.OrderItems)
                    .HasForeignKey(oi => oi.Status);
        }
    }
}
