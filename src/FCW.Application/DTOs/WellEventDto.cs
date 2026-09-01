using System;
using System.Collections.Generic;
using System.Text;

namespace FCW.Application.DTOs;

public class WellEventDto
{
    public int Id { get; set; }
    public int DesignConceptId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? PlannedStartDate { get; set; }
    public DateTime? PlannedEndDate { get; set; }
    public string Notes { get; set; } = string.Empty;

    // Drilling
    public decimal? PlannedDepth { get; set; }
    public string? MudType { get; set; }

    // Completion
    public string? CompletionType { get; set; }
    public decimal? TubingSize { get; set; }

    // Intervention
    public string? InterventionReason { get; set; }
    public string? ToolUsed { get; set; }

    // Abandonment
    public decimal? PlugDepth { get; set; }
    public string? AbandonmentReason { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}