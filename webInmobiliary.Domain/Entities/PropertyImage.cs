namespace webInmobiliary.Domain.Entities;

public class PropertyImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public int  PropertyId { get; set; }
    public Property Property { get; set; } = new Property();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}