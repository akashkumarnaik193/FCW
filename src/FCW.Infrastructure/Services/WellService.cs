using System;
using System.Collections.Generic;
using System.Text;

using FCW.Application.DTOs;
using FCW.Application.Interfaces;
using FCW.Domain.Entities;
using FCW.Domain.Enums;
using FCW.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FCW.Infrastructure.Services;

public class WellService : IWellService
{
    private readonly FcwDbContext _context;

    public WellService(FcwDbContext context)
    {
        _context = context;
    }

    public async Task<List<WellDto>> GetAllAsync(string? search, int page, int pageSize)
    {
        var query = _context.Wells.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(w =>
                w.WellName.Contains(search) ||
                w.Field.Contains(search) ||
                w.Country.Contains(search));
        }

        return await query
            .OrderBy(w => w.WellName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(w => MapToDto(w))
            .ToListAsync();
    }

    public async Task<WellDto?> GetByIdAsync(int id)
    {
        var well = await _context.Wells.FindAsync(id);
        return well is null ? null : MapToDto(well);
    }

    public async Task<WellDto> CreateAsync(CreateWellDto dto, string createdBy)
    {
        var well = new Well
        {
            WellName = dto.WellName,
            Field = dto.Field,
            Asset = dto.Asset,
            Country = dto.Country,
            Operator = dto.Operator,
            WellType = Enum.Parse<WellType>(dto.WellType, ignoreCase: true),
            Status = WellStatus.Planned,
            CreatedBy = createdBy,
            CreatedDate = DateTime.UtcNow
        };

        _context.Wells.Add(well);
        await _context.SaveChangesAsync();

        return MapToDto(well);
    }

    private static WellDto MapToDto(Well w) => new()
    {
        Id = w.Id,
        WellName = w.WellName,
        Field = w.Field,
        Asset = w.Asset,
        Country = w.Country,
        Operator = w.Operator,
        WellType = w.WellType.ToString(),
        Status = w.Status.ToString(),
        CreatedBy = w.CreatedBy,
        CreatedDate = w.CreatedDate
    };
}
