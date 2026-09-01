using System;
using System.Collections.Generic;
using System.Text;

using FCW.Application.DTOs;
using FCW.Application.Interfaces;
using FCW.Domain.Entities;
using FCW.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FCW.Infrastructure.Services;

public class DesignConceptService : IDesignConceptService
{
    private readonly FcwDbContext _context;

    public DesignConceptService(FcwDbContext context)
    {
        _context = context;
    }

    public async Task<List<DesignConceptDto>> GetByWellIdAsync(int wellId)
    {
        return await _context.DesignConcepts
            .Where(d => d.WellId == wellId)
            .Include(d => d.Well)
            .OrderByDescending(d => d.CreatedDate)
            .Select(d => MapToDto(d))
            .ToListAsync();
    }

    public async Task<DesignConceptDto?> GetByIdAsync(int wellId, int designId)
    {
        var design = await _context.DesignConcepts
            .Include(d => d.Well)
            .FirstOrDefaultAsync(d => d.Id == designId && d.WellId == wellId);

        return design is null ? null : MapToDto(design);
    }

    public async Task<DesignConceptDto?> CreateAsync(int wellId, CreateDesignConceptDto dto, string createdBy)
    {
        var wellExists = await _context.Wells.AnyAsync(w => w.Id == wellId);
        if (!wellExists)
            return null; // Controller will translate this into a 404

        var design = new DesignConcept
        {
            WellId = wellId,
            Name = dto.Name,
            Description = dto.Description,
            CreatedBy = createdBy,
            CreatedDate = DateTime.UtcNow
        };

        _context.DesignConcepts.Add(design);
        await _context.SaveChangesAsync();

        // Reload with Well included so the DTO can show WellName
        await _context.Entry(design).Reference(d => d.Well).LoadAsync();
        return MapToDto(design);
    }

    public async Task<DesignConceptDto?> UpdateAsync(int wellId, int designId, UpdateDesignConceptDto dto, string modifiedBy)
    {
        var design = await _context.DesignConcepts
            .Include(d => d.Well)
            .FirstOrDefaultAsync(d => d.Id == designId && d.WellId == wellId);

        if (design is null)
            return null;

        design.Name = dto.Name;
        design.Description = dto.Description;
        design.ModifiedBy = modifiedBy;
        design.ModifiedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToDto(design);
    }

    public async Task<bool> DeleteAsync(int wellId, int designId)
    {
        var design = await _context.DesignConcepts
            .FirstOrDefaultAsync(d => d.Id == designId && d.WellId == wellId);

        if (design is null)
            return false;

        _context.DesignConcepts.Remove(design);
        await _context.SaveChangesAsync();
        return true;
    }

    private static DesignConceptDto MapToDto(DesignConcept d) => new()
    {
        Id = d.Id,
        WellId = d.WellId,
        WellName = d.Well?.WellName ?? string.Empty,
        Name = d.Name,
        Description = d.Description,
        Status = d.Status.ToString(),
        Version = d.Version,
        CreatedBy = d.CreatedBy,
        CreatedDate = d.CreatedDate
    };
}
