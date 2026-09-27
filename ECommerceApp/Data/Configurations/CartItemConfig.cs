using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Data.Configurations;

public class CartItemConfig : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItem");
        
        builder.HasKey(cartItem => cartItem.Id);

        builder.HasIndex(cartItem => cartItem.Id)
                .IsUnique();

        builder.Property(cartItem => cartItem.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

        builder.Property(cartItem => cartItem.Quantity)
                .IsRequired();

        builder.HasOne(cartItem => cartItem.cart)
                .WithMany(cart => cart.cartItems)
                .HasForeignKey(carItem => carItem.CartId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(cartItem => cartItem.product)
                .WithMany(cart => cart.cartItems)
                .HasForeignKey(carItem => carItem.CartId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}