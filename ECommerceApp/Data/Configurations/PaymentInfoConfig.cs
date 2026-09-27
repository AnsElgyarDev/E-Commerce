using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Data.Configurations;

public class PaymentInfoConfig : IEntityTypeConfiguration<PaymentInfo>
{
    public void Configure(EntityTypeBuilder<PaymentInfo> builder)
    {
        builder.ToTable("PaymentInfo");
        
        builder.HasKey(paymentInfo=> paymentInfo.Id);

        builder.HasIndex(paymentInfo => paymentInfo.Id)
                .IsUnique();

        builder.Property(paymentInfo => paymentInfo.Id)
                .UseIdentityColumn(seed: 1, increment: 1);
        
        builder.Property(paymentInfo => paymentInfo.PaymentMethod)
                .IsRequired();
        
        builder.Property(paymentInfo => paymentInfo.Status)
                .IsRequired();

        builder.HasOne(paymentInfo => paymentInfo.order)
                .WithOne(order => order.paymentInfo)
                .OnDelete(DeleteBehavior.SetNull);
    }
}