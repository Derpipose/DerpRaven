﻿using DerpRaven.Api.Model;
using Microsoft.EntityFrameworkCore;
namespace DerpRaven.Api.Services;

public class CustomRequestService
{
    private AppDbContext _context;
    private ILogger _logger;

    public CustomRequestService(AppDbContext context, ILogger<CustomRequestService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CustomRequest>> GetAllCustomRequestsAsync()
    {
        return await _context.CustomRequests.ToListAsync();
    }

    public async Task<IEnumerable<CustomRequest>> GetCustomRequestsByUserAsync(int id)
    {
        return await _context.CustomRequests.Where(r => r.User.Id == id).ToListAsync();
    }

    public async Task<CustomRequest?> GetCustomRequestByIdAsync(int id)
    {
        return await _context.CustomRequests.FindAsync(id);
    }

    public async Task<IEnumerable<CustomRequest>> GetCustomRequestsByStatusAsync(string status)
    {
        return await _context.CustomRequests.Where(r => r.Status == status).ToListAsync();
    }

    public async Task<IEnumerable<CustomRequest>> GetCustomRequestsByTypeAsync(string productType)
    {
        return await _context.CustomRequests.Where(r => r.ProductType.Name == productType).ToListAsync();
    }

    public async Task ChangeStatusAsync(int id, string status)
    {
        var request = await _context.CustomRequests.FindAsync(id);
        if (request != null)
        {
            request.Status = status;
            await _context.SaveChangesAsync();
        }
    }

    public async Task CreateCustomRequestAsync(string description, string email, string status, int productTypeId, int userId)
    {
        var productType = _context.ProductTypes.Where(t => t.Id == productTypeId).Single();
        var user = _context.Users.Where(u => u.Id == userId).Single();

        var customRequest = new CustomRequest()
        {
            Description = description,
            Email = email,
            Status = status,
            ProductType = productType,
            User = user
        };
        await _context.CustomRequests.AddAsync(customRequest);
        await _context.SaveChangesAsync();
    }
}
