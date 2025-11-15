using webInmobiliary.Application.Dto;
using webInmobiliary.Application.Interfaces;
using webInmobiliary.Domain.Entities;
using webInmobiliary.Domain.Interfaces;

namespace webInmobiliary.Application.Services;

public class PropertyService : IPropertyService
{
    // inyectar infrastrucre service 

    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }



    public async Task<IEnumerable<PropertyDto>> GetAllProperty()
    {
        var propert = await _propertyRepository.GetAllProperty();
        return propert.Select(p => new PropertyDto
        {
            Id = p.Id,
            Description = p.Description,
            Location = p.Location,
            Price = p.Price,
            // ImageUrls = p.Images.,
            Title = p.Title,
            Admin = p.Admin
        });
    }

    public async Task<PropertyDto> GetIdProperty(int id)
    {
        var propert = await _propertyRepository.GetIdProperty(id);
        return new PropertyDto
        {
            Id = propert.Id,
            Description = propert.Description,
            Location = propert.Location,
            Price = propert.Price,
            // ImageUrls = propert.ImageUrls,
            Title = propert.Title,
            Admin =propert.Admin
        };
    }

    public async Task<PropertyDto> AddProperty(PropertyCreateDto propertyCreateDto)
    {
        var newPropert = new Property
        {
            Description = propertyCreateDto.Description,
            Location = propertyCreateDto.Location,
            Price = propertyCreateDto.Price,
            // ImageUrls = propertyCreateDto.ImageUrls,
            Title = propertyCreateDto.Title,
            Admin = propertyCreateDto.Admin
            
        };

        await _propertyRepository.AddProperty(newPropert);
        return new PropertyDto
        {
            Id = newPropert.Id,
            Description = newPropert.Description,
            Location = newPropert.Location,
            Price = newPropert.Price,
            // ImageUrls = newPropert.ImageUrls,
            Title = newPropert.Title,
            Admin = newPropert.Admin
        };
    }

    public async Task<PropertyDto> UpdateProperty(int id, PropertyUpdateDto propertyUpdateDto)
    {
        var result = await _propertyRepository.GetIdProperty(id);
        if (result == null) return null;

        result.Title = propertyUpdateDto.Title;
        result.Description = propertyUpdateDto.Description;
        result.Price = propertyUpdateDto.Price;
        result.Location = propertyUpdateDto.Location;
        // result.ImageUrls = propertyUpdateDto.ImageUrls;
        result.Admin = propertyUpdateDto.Admin;

        await _propertyRepository.UpdateProperty(result);

        return new PropertyDto
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            Price = result.Price,
            Location = result.Location,
            // ImageUrls = result.ImageUrls,
            Admin = result.Admin

        };


    }

    public async Task<bool> DeleteProperty(int id)
    {
        return await _propertyRepository.DeleteProperty(id);
    }
}