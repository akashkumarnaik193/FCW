using System;
using System.Collections.Generic;
using System.Text;

namespace FCW.Application.DTOs;

public class WellDto
{
    public int Id { get; set; }
    public string WellName { get; set; } = string.Empty;
    public string Field { get; set; } = string.Empty;
    public string Asset { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Operator { get; set; } = string.Empty;
    public string WellType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
