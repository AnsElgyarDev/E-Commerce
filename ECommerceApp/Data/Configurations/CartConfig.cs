using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Data.Configurations;

public class CartConfig : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Cart");
        
        builder.HasKey(cart => cart.Id);

        builder.HasIndex(cart => cart.Id)
                .IsUnique();

        builder.Property(cart => cart.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

        builder.HasOne(cart => cart.user)
                .WithOne(user => user.cart)
                .OnDelete(DeleteBehavior.Cascade);
    }
}