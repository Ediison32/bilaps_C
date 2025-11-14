using webInmobiliary.Domain.Entities;

namespace webInmobiliary.Application.Dto;

public class PropertyDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new List<string>();
    public Admin Admin { get; set; } = null!;
}

public class PropertyCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new List<string>();
    public Admin Admin { get; set; } = null!;
  
}

public class PropertyUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new List<string>();
    public Admin Admin { get; set; } = null!;
}