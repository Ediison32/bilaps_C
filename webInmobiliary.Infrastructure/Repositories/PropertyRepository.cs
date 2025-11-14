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
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteProperty(int id)
    {
        _appDbContext.Remove(id);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}