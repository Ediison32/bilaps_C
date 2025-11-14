using Microsoft.EntityFrameworkCore;
using webInmobiliary.Domain.Entities;
using webInmobiliary.Domain.Interfaces;
using webInmobiliary.Infrastructure.Data;

namespace webInmobiliary.Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Admin?> GetByIdAsync(int id)
    {
        return await _context.Admins.FindAsync(id);
    }

    public async Task<IEnumerable<Admin>> GetAllAsync()
    {
        return await _context.Admins.ToListAsync();
    }
}