using Microsoft.AspNetCore.Mvc;
using webInmobiliary.Application.Dto;
using webInmobiliary.Application.Interfaces;

namespace webInmobiliary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyControlle : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyControlle(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllProperty()
    {
        try
        {
            var resul = await _propertyService.GetAllProperty();
            return Ok(resul);
        }
        catch (HttpRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIdProperty(int id)
    
    {
        try
        {
            var result = await _propertyService.GetIdProperty(id);
            return Ok(result);
            
        }
        catch (HttpRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(int id)
                      
    {
        try
        {
            var result = await _propertyService.DeleteProperty(id);
            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

  
    [HttpPost("Create")]
    public async Task<IActionResult> CreateProperty([FromBody] PropertyCreateDto dto)
    {
        try
        {
            var result = await _propertyService.AddProperty(dto);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProperty(int id, [FromBody] PropertyUpdateDto dto)
    {
        try
        {
            var result = await _propertyService.UpdateProperty(id, dto);

            if (result == null)
            {
                return NotFound(new { message = "La propiedad no existe." });
            }

            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}