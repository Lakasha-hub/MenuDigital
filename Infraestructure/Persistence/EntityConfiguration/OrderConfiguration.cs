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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.OrderId);
            builder.Property(o => o.OrderId).HasColumnType("bigint").ValueGeneratedOnAdd();
            builder.Property(o => o.DeliveryTo).IsRequired().HasMaxLength(255);
            builder.Property(o => o.Notes).HasColumnType("text");
            builder.Property(o => o.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.CreateDate)
                  .HasColumnType("timestamp")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(o => o.UpdateDate)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasMany<OrderItem>(o => o.OrderItems)
                  .WithOne(oi => oi.OrderDb)
                  .HasForeignKey(oi => oi.Order);

            builder.HasOne<DeliveryType>(o => o.DeliveryTypeDb)
                    .WithMany(dt => dt.Orders)
                    .HasForeignKey(o => o.DeliveryType);

            builder.HasOne<Status>(o => o.StatusDb)
                    .WithMany(s => s.Orders)
                    .HasForeignKey(o => o.OverallStatus);
        }
    }
}
