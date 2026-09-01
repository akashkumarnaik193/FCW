using System;
using System.Collections.Generic;
using System.Text;

using FCW.Domain.Common;

namespace FCW.Domain.Entities;

public class Casing : AuditableEntity
{
    public int WellEventId { get; set; }
    public WellEvent WellEvent { get; set; } = null!;

    public string CasingType { get; set; } = string.Empty;
    public decimal Diameter { get; set; }
    public string Grade { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public decimal Depth { get; set; }
    public string Material { get; set; } = string.Empty;
    public string Connection { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}