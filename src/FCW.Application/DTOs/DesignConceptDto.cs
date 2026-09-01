using System;
using System.Collections.Generic;
using System.Text;

namespace FCW.Application.DTOs;

public class DesignConceptDto
{
    public int Id { get; set; }
    public int WellId { get; set; }
    public string WellName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int Version { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
