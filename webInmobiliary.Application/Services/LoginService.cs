using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webInmobiliary.Application.Dto;
using webInmobiliary.Application.Interfaces;
using webInmobiliary.Domain.Entities;
using webInmobiliary.Domain.Interfaces;

namespace webInmobiliary.Application.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly DbContext _context;

    public LoginService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        DbContext context)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _context = context;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new Exception("El usuario ya existe");

        User newUser = request.Role switch
        {
            Role.Admin => new Admin { Name = request.Name, Email = request.Email },
            Role.Client => new Client { Name = request.Name, Email = request.Email, Phone = request.Phone ?? "" },
            _ => throw new Exception("Rol no válido")
        };

        newUser.PasswordHash = _passwordHasher.HashPassword(request.Password);
        newUser.RefreshToken = _jwtService.GenerateRefreshToken();
        newUser.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await _userRepository.AddAsync(newUser);

        var token = _jwtService.GenerateToken(newUser);
        
        return new AuthResponse
        {
            Token = token,
            RefreshToken = newUser.RefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60),
            Email = newUser.Email,
            Name = newUser.Name,
            Role = newUser.Role.ToString()
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
            throw new UnauthorizedAccessException("Credenciales inválidas");

        var token = _jwtService.GenerateToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);

        return new AuthResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60),
            Email = user.Email,
            Name = user.Name,
            Role = user.Role.ToString()
        };
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var user = await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow);

        if (user == null)
            throw new SecurityTokenException("Refresh token inválido o expirado");

        var newToken = _jwtService.GenerateToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);

        return new AuthResponse
        {
            Token = newToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60),
            Email = user.Email,
            Name = user.Name,
            Role = user.Role.ToString()
        };
    }

    public async Task<bool> RevokeTokenAsync(string refreshToken)
    {
        var user = await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        if (user != null)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        return false;
    }
}