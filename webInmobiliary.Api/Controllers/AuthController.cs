using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webInmobiliary.Application.Dto;
using webInmobiliary.Application.Interfaces;

namespace webInmobiliary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService;

    public AuthController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("/api/v1/auth/register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var result = await _loginService.RegisterAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("/api/v1/auth/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _loginService.LoginAsync(request);
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Credenciales inválidas" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var result = await _loginService.RefreshTokenAsync(request.RefreshToken);
            return Ok(result);
        }
        catch (SecurityTokenException)
        {
            return Unauthorized(new { message = "Refresh token inválido" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _loginService.RevokeTokenAsync(request.RefreshToken);
        if (result)
            return Ok(new { message = "Token revocado exitosamente" });
        
        return BadRequest(new { message = "No se pudo revocar el token" });
    }
}