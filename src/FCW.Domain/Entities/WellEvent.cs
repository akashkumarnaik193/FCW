using System;
using System.Collections.Generic;
using System.Text;

using FCW.Domain.Common;
using FCW.Domain.Enums;

namespace FCW.Domain.Entities;

public class WellEvent : AuditableEntity
{
    public int DesignConceptId { get; set; }
    public DesignConcept DesignConcept { get; set; } = null!;

    public WellEventType EventType { get; set; }
    public EventStatus Status { get; set; } = EventStatus.Planned;
    public DateTime? PlannedStartDate { get; set; }
    public DateTime? PlannedEndDate { get; set; }
    public string Notes { get; set; } = string.Empty;

    public decimal? PlannedDepth { get; set; }
    public string? MudType { get; set; }

    public string? CompletionType { get; set; }
    public decimal? TubingSize { get; set; }

    public string? InterventionReason { get; set; }
    public string? ToolUsed { get; set; }

    public decimal? PlugDepth { get; set; }
    public string? AbandonmentReason { get; set; }

    public ICollection<Casing> Casings { get; set; } = new List<Casing>();
    public UpperCompletion? UpperCompletion { get; set; }
}