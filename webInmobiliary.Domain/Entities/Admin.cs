namespace webInmobiliary.Domain.Entities;

public class Admin : User
{
    public Admin()
    {
        Role = Role.Admin;
    }
    
    public bool IsSuperAdmin { get; set; } = false;
}