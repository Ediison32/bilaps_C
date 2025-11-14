using System.ComponentModel.DataAnnotations;

namespace webInmobiliary.Domain.Entities;

public class Property
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    
    [Required]
    [MaxLength(300)]
    public string Location { get; set; } = string.Empty;
    
    // URLs de imágenes en Cloudinary - almacenar como JSON en BD
    public List<string> ImageUrls { get; set; } = new List<string>();
    
    // Relación con Admin (solo admins crean propiedades)
    public int AdminId { get; set; }
    public Admin Admin { get; set; } = null!;
    
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}