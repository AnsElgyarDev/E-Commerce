using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ECommerceApp.Models;

namespace ECommerceApp.Data;

public class AppDbContext : IdentityDbContext<AppliactionUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AppliactionUser> users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> carts { get; set; }
    public DbSet<Category> categories { get; set; }
    public DbSet<CartItem> cartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PaymentInfo> paymentInfos { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var configuartions = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var connectionString = configuartions.GetConnectionString("DefaultConnection");
        
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}