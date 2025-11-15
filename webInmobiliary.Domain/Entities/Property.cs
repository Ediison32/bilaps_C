namespace webInmobiliary.Domain.Entities;

public class Property
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    // Relación con Admin (solo admins crean propiedades)
    public int AdminId { get; set; }
    public Admin Admin { get; set; } = null!;
    
    // Tabla pivote para imágenes (mejor que List<string>)
    public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
}