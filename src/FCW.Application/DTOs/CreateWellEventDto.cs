using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace FCW.Application.DTOs;

public class CreateWellEventDto
{
    [Required]
    public string EventType { get; set; } = string.Empty; // Drilling/Completion/Intervention/Abandonment

    public DateTime? PlannedStartDate { get; set; }
    public DateTime? PlannedEndDate { get; set; }

    [MaxLength(2000)]
    public string Notes { get; set; } = string.Empty;

    // Drilling
    public decimal? PlannedDepth { get; set; }
    [MaxLength(100)]
    public string? MudType { get; set; }

    // Completion
    [MaxLength(100)]
    public string? CompletionType { get; set; }
    public decimal? TubingSize { get; set; }

    // Intervention
    [MaxLength(500)]
    public string? InterventionReason { get; set; }
    [MaxLength(200)]
    public string? ToolUsed { get; set; }

    // Abandonment
    public decimal? PlugDepth { get; set; }
    [MaxLength(500)]
    public string? AbandonmentReason { get; set; }
}