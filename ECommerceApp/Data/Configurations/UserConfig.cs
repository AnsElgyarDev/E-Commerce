using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Data.Configurations;

public class UserConfig : IEntityTypeConfiguration<AppliactionUser>
{
    public void Configure(EntityTypeBuilder<AppliactionUser> builder)
    {
        builder.ToTable("User");
        
        builder.HasKey(user => user.Id);

        builder.Property(user=> user.FullName)
        .HasMaxLength(200)
        .IsRequired();

        builder.HasIndex(user=> user.Id)
        .IsUnique();

        builder.Property(user=>user.Id)
        .UseIdentityColumn(seed: 1, increment: 1);

        builder.Property(user => user.Email)
        .HasMaxLength(50)
        .IsRequired();
    }
}