namespace webInmobiliary.Domain.Entities;

public class Client : User
{
    public string Phone { get; set; } = string.Empty;
    public bool AcceptsContact { get; set; } = true;
}