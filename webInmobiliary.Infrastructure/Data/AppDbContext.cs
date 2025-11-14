using Microsoft.EntityFrameworkCore;
using webInmobiliary.Domain.Entities;

namespace webInmobiliary.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de herencia TPH para User
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasDiscriminator<Role>(u => u.Role)
            .HasValue<Admin>(Role.Admin)
            .HasValue<Client>(Role.Client);

        // Configuración de Property
        modelBuilder.Entity<Property>(entity =>
        {
            entity.ToTable("Properties");
            
            // Configurar ImageUrls como JSON para MySQL
            entity.Property(p => p.ImageUrls)
                .HasConversion(
                    v => string.Join(';', v), // Convert List to string
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .HasColumnType("text");

            // Relación con Admin
            entity.HasOne(p => p.Admin)
                .WithMany()
                .HasForeignKey(p => p.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Unique constraints
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Property>()
            .HasIndex(p => p.Title);

        // Configuración de índices para performance
        modelBuilder.Entity<Property>()
            .HasIndex(p => new { p.IsActive, p.Price });
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Property> Properties { get; set; }
}