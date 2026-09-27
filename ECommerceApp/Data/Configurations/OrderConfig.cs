using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Data.Configurations;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");
        
        builder.HasKey(order => order.Id);

        builder.Property(order => order.Status)
                .IsRequired();

        builder.HasIndex(order => order.Id)
                .IsUnique();

        builder.Property(order => order.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

        builder.Property(order => order.TotalPrice)
                .HasPrecision(18, 2)
                .IsRequired();

        builder.HasOne(order => order.user)
                .WithMany(user => user.orders)
                .HasForeignKey(order => order.UserId)
                .OnDelete(DeleteBehavior.SetNull);
    }
}