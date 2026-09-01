using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FCW.Domain.Common;
using FCW.Domain.Enums;

namespace FCW.Domain.Entities;

public class DesignConcept : AuditableEntity
{
    public int WellId { get; set; }
    public Well Well { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DesignStatus Status { get; set; } = DesignStatus.Draft;
    public int Version { get; set; } = 1;

    public ICollection<WellEvent> WellEvents { get; set; } = new List<WellEvent>();
}