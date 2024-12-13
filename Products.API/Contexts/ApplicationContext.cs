using Microsoft.EntityFrameworkCore;
using Products.API.Models;

namespace Products.API.Contexts;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options){}

    public DbSet<Category> categories { get; set; }
    public DbSet<Product> products  { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(c => c.categoryid);

        modelBuilder.Entity<Product>().
            Property(c => c.name).
            HasMaxLength(100).
            IsRequired();

        modelBuilder.Entity<Product>().
            Property(c => c.description).
            HasMaxLength(255).
            IsRequired();

        modelBuilder.Entity<Product>().
            Property(c => c.imageurl).
            HasMaxLength(255).
            IsRequired();

        modelBuilder.Entity<Product>().
            Property(c => c.price).
            HasPrecision(12, 2).
            IsRequired();

        modelBuilder.Entity<Category>().
            HasMany(g => g.products).
            WithOne(p => p.category).
            IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                categoryid = 1,
                name = "Material Escolar"
            },
            new Category
            {
                categoryid = 2,
                name = "Acessórios"
            }
        );
    }
}
