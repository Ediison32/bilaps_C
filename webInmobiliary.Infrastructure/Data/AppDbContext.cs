using Microsoft.EntityFrameworkCore;
using webInmobiliary.Domain.Entities;

namespace webInmobiliary.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPH para User - UNA tabla users con discriminador
        modelBuilder.Entity<User>()
            .ToTable("users")
            .HasDiscriminator<Role>(u => u.Role)
            .HasValue<Admin>(Role.Admin)
            .HasValue<Client>(Role.Client);

        // Tablas separadas para el resto
        modelBuilder.Entity<Property>().ToTable("properties");
        modelBuilder.Entity<PropertyImage>().ToTable("property_images");
        
        // Configuración de Users
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Name).HasMaxLength(100).IsRequired();
            entity.Property(u => u.Email).HasMaxLength(150).IsRequired();
            entity.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired();
        });
        
        // Configuración de Properties
        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasIndex(p => p.Title);
            entity.HasIndex(p => new { p.IsActive, p.Price });
            entity.HasIndex(p => p.Location);
            
            entity.Property(p => p.Title).HasMaxLength(200).IsRequired();
            entity.Property(p => p.Description).HasMaxLength(1000);
            entity.Property(p => p.Location).HasMaxLength(300).IsRequired();
            entity.Property(p => p.Price).HasPrecision(18, 2);
            
            // Relación con Admin
            entity.HasOne(p => p.Admin)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // Configuración de PropertyImages
        modelBuilder.Entity<PropertyImage>(entity =>
        {
            entity.HasIndex(pi => pi.PropertyId);
            entity.HasIndex(pi => new { pi.PropertyId, pi.DisplayOrder });
            
            entity.Property(pi => pi.ImageUrl).HasMaxLength(500).IsRequired();
            
            // Relación con Property
            entity.HasOne(pi => pi.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
}