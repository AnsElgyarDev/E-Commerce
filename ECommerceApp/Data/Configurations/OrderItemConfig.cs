using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Data.Configurations;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");
        
        builder.HasKey(orderItem => orderItem.Id);

        builder.HasIndex(orderItem => orderItem.Id)
                .IsUnique();

        builder.Property(orderItem => orderItem.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

        builder.Property(orderItem => orderItem.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

        builder.HasOne(orderItem => orderItem.order)
                .WithMany(order => order.orderItems)
                .HasForeignKey(orderItem => orderItem.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(orderItem => orderItem.product)
                .WithMany(product => product.orderItems)
                .HasForeignKey(orderItem => orderItem.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}