using System.ComponentModel.DataAnnotations;

namespace webInmobiliary.Domain.Entities;

public class Client : User
{
    public Client()
    {
        Role = Role.Client;
    }
    
    [Phone]
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;
    
    // Para el feature de "Contactar" del enunciado
    public bool AcceptsContact { get; set; } = true;
}