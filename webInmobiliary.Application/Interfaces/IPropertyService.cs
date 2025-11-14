using webInmobiliary.Application.Dto;
using webInmobiliary.Domain.Entities;

namespace webInmobiliary.Application.Interfaces;

public interface IPropertyService
{
    Task<IEnumerable<PropertyDto>> GetAllProperty();
    Task<PropertyDto> GetIdProperty(int id);
    
    Task<PropertyDto> AddProperty(PropertyCreateDto propertyCreateDto);
    Task<PropertyDto> UpdateProperty(int id, PropertyUpdateDto propertyUpdateDto);
    Task<Boolean> DeleteProperty(int id);   
}