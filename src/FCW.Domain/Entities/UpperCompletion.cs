using System;
using System.Collections.Generic;
using System.Text;

using FCW.Domain.Common;

namespace FCW.Domain.Entities;

public class UpperCompletion : AuditableEntity
{
    public int WellEventId { get; set; }
    public WellEvent WellEvent { get; set; } = null!;

    public string ComponentConfiguration { get; set; } = string.Empty;
    public string TubingType { get; set; } = string.Empty;
    public decimal? TubingLength { get; set; }
    public string PackerType { get; set; } = string.Empty;
}
