using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Data.Configurations;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name)
                .HasMaxLength(200)
                .IsRequired();

        builder.HasIndex(product => product.Id)
                .IsUnique();

        builder.Property(product => product.Id)
                .UseIdentityColumn(seed: 1, increment: 1);

        builder.Property(product => product.Price)
                .HasPrecision(18, 2)
                .IsRequired();

        builder.HasOne(product => product.category)
                .WithMany(category => category.products)
                .HasForeignKey(product=> product.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}