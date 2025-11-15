namespace webInmobiliary.Domain.Entities;

public class Admin : User
{
    public bool IsSuperAdmin { get; set; } = false;
    
    // Navigation property - un Admin puede crear muchas Properties
    public ICollection<Property> Properties { get; set; } = new List<Property>();
}