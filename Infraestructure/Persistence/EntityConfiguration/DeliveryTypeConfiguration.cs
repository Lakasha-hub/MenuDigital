﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.EntityConfiguration
{
    public class DeliveryTypeConfiguration : IEntityTypeConfiguration<DeliveryType>
    {
        public void Configure(EntityTypeBuilder<DeliveryType> builder)
        {
            builder.ToTable("DeliveryType");

            builder.HasKey(dt => dt.Id);
            builder.Property(dt => dt.Id).ValueGeneratedOnAdd();
            builder.Property(dt => dt.Name).IsRequired().HasMaxLength(25);

            builder.HasData(
                new DeliveryType
                {
                    Id = 1,
                    Name = "Delivery"
                },
                new DeliveryType
                {
                    Id = 2,
                    Name = "Take away"
                },
                new DeliveryType
                {
                    Id = 3,
                    Name = "Dine in"
                }
            );
        }
    }
}
