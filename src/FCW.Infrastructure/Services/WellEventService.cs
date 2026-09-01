using System;
using System.Collections.Generic;
using System.Text;

using FCW.Application.DTOs;
using FCW.Application.Exceptions;
using FCW.Application.Interfaces;
using FCW.Domain.Entities;
using FCW.Domain.Enums;
using FCW.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FCW.Infrastructure.Services;

public class WellEventService : IWellEventService
{
    private readonly FcwDbContext _context;

    public WellEventService(FcwDbContext context)
    {
        _context = context;
    }

    public async Task<List<WellEventDto>> GetByDesignIdAsync(int wellId, int designId)
    {
        return await _context.WellEvents
            .Include(e => e.DesignConcept)
            .Where(e => e.DesignConceptId == designId && e.DesignConcept.WellId == wellId)
            .OrderBy(e => e.CreatedDate)
            .Select(e => MapToDto(e))
            .ToListAsync();
    }

    public async Task<WellEventDto?> GetByIdAsync(int wellId, int designId, int eventId)
    {
        var wellEvent = await _context.WellEvents
            .Include(e => e.DesignConcept)
            .FirstOrDefaultAsync(e =>
                e.Id == eventId &&
                e.DesignConceptId == designId &&
                e.DesignConcept.WellId == wellId);

        return wellEvent is null ? null : MapToDto(wellEvent);
    }

    public async Task<WellEventDto?> CreateAsync(int wellId, int designId, CreateWellEventDto dto, string createdBy)
    {
        var designExists = await _context.DesignConcepts
            .AnyAsync(d => d.Id == designId && d.WellId == wellId);

        if (!designExists)
            return null; // Controller maps this to 404

        if (!Enum.TryParse<WellEventType>(dto.EventType, ignoreCase: true, out var eventType))
            throw new InvalidWellEventException($"'{dto.EventType}' is not a valid event type. Use Drilling, Completion, Intervention, or Abandonment.");

        ValidateTypeSpecificFields(eventType, dto);

        var wellEvent = new WellEvent
        {
            DesignConceptId = designId,
            EventType = eventType,
            Status = EventStatus.Planned,
            PlannedStartDate = dto.PlannedStartDate,
            PlannedEndDate = dto.PlannedEndDate,
            Notes = dto.Notes,
            PlannedDepth = dto.PlannedDepth,
            MudType = dto.MudType,
            CompletionType = dto.CompletionType,
            TubingSize = dto.TubingSize,
            InterventionReason = dto.InterventionReason,
            ToolUsed = dto.ToolUsed,
            PlugDepth = dto.PlugDepth,
            AbandonmentReason = dto.AbandonmentReason,
            CreatedBy = createdBy,
            CreatedDate = DateTime.UtcNow
        };

        _context.WellEvents.Add(wellEvent);
        await _context.SaveChangesAsync();

        return MapToDto(wellEvent);
    }

    public async Task<bool> DeleteAsync(int wellId, int designId, int eventId)
    {
        var wellEvent = await _context.WellEvents
            .Include(e => e.DesignConcept)
            .FirstOrDefaultAsync(e =>
                e.Id == eventId &&
                e.DesignConceptId == designId &&
                e.DesignConcept.WellId == wellId);

        if (wellEvent is null)
            return false;

        _context.WellEvents.Remove(wellEvent);
        await _context.SaveChangesAsync();
        return true;
    }

    // This is where the TPH tradeoff gets paid back - required fields per type, enforced here
    private static void ValidateTypeSpecificFields(WellEventType type, CreateWellEventDto dto)
    {
        switch (type)
        {
            case WellEventType.Drilling:
                if (dto.PlannedDepth is null)
                    throw new InvalidWellEventException("PlannedDepth is required for a Drilling event.");
                break;

            case WellEventType.Completion:
                if (string.IsNullOrWhiteSpace(dto.CompletionType))
                    throw new InvalidWellEventException("CompletionType is required for a Completion event.");
                break;

            case WellEventType.Intervention:
                if (string.IsNullOrWhiteSpace(dto.InterventionReason))
                    throw new InvalidWellEventException("InterventionReason is required for an Intervention event.");
                break;

            case WellEventType.Abandonment:
                if (dto.PlugDepth is null)
                    throw new InvalidWellEventException("PlugDepth is required for an Abandonment event.");
                break;
        }
    }

    private static WellEventDto MapToDto(WellEvent e) => new()
    {
        Id = e.Id,
        DesignConceptId = e.DesignConceptId,
        EventType = e.EventType.ToString(),
        Status = e.Status.ToString(),
        PlannedStartDate = e.PlannedStartDate,
        PlannedEndDate = e.PlannedEndDate,
        Notes = e.Notes,
        PlannedDepth = e.PlannedDepth,
        MudType = e.MudType,
        CompletionType = e.CompletionType,
        TubingSize = e.TubingSize,
        InterventionReason = e.InterventionReason,
        ToolUsed = e.ToolUsed,
        PlugDepth = e.PlugDepth,
        AbandonmentReason = e.AbandonmentReason,
        CreatedBy = e.CreatedBy,
        CreatedDate = e.CreatedDate
    };
}