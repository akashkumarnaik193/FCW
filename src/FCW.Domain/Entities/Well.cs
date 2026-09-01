using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FCW.Domain.Common;
using FCW.Domain.Enums;

namespace FCW.Domain.Entities;

public class Well : AuditableEntity
{
    public string WellName { get; set; } = string.Empty;
    public string Field { get; set; } = string.Empty;
    public string Asset { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Operator { get; set; } = string.Empty;
    public WellType WellType { get; set; }
    public WellStatus Status { get; set; } = WellStatus.Planned;

    // Navigation property: one Well has many DesignConcepts
    public ICollection<DesignConcept> DesignConcepts { get; set; } = new List<DesignConcept>();
}