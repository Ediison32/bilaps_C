using Microsoft.EntityFrameworkCore;
using webInmobiliary.Domain.Entities;
using webInmobiliary.Domain.Interfaces;
using webInmobiliary.Infrastructure.Data;

namespace webInmobiliary.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    
    // inyectar db 
    private readonly AppDbContext _appDbContext;
    
    public PropertyRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    
    public async Task<IEnumerable<Property>> GetAllProperty()
    {
        return await _appDbContext.Properties.ToListAsync();
    }

    public async Task<Property> GetIdProperty(int id)
    {
        Console.WriteLine("entro a mirar la db");
        var result = await _appDbContext.Properties.FindAsync(id);
        if (result == null) return null;
        return result;
    }

    public async Task<Property> AddProperty(Property property)
    {
        _appDbContext.Properties.AddAsync(property);
        await _appDbContext.SaveChangesAsync();
        return property;
    }

    public async Task<Property> UpdateProperty(Property property)
    {
        var existing = await _appDbContext.Properties.FindAsync(property.Id);

        if (existing == null)
            return null!; // o lanzar una excepción si así manejas errores
        
        existing.Title = property.Title;
        existing.Description = property.Description;
        existing.Price = property.Price;
        existing.Location = property.Location;
        existing.ImageUrls = property.ImageUrls;
        existing.AdminId = property.AdminId;

        await _appDbContext.SaveChangesAsync();

        return existing;
    }
    public async Task<bool> DeleteProperty(int id)
    {
        var deleted = await _appDbContext.Properties.FindAsync(id);
        if (deleted == null) return false;
        _appDbContext.Properties.Remove(deleted);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}