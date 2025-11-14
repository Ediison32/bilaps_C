using webInmobiliary.Domain.Entities;

namespace webInmobiliary.Domain.Interfaces;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllProperty();
    Task<Property> GetIdProperty(int id);
    
    Task<Property> AddProperty(Property property);
    Task<Property> UpdateProperty(Property property);
    Task<Boolean> DeleteProperty(int id);   
}