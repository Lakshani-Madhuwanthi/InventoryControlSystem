
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;

public class InventoryDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the column type for the ProductPrice property
        modelBuilder.Entity<Product>()
            .Property(p => p.ProductPrice)
            .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

        // Add any additional configuration here

        base.OnModelCreating(modelBuilder);
    }
}
