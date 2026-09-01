using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace FCW.Application.DTOs;

public class CreateWellDto
{
    [Required, MaxLength(200)]
    public string WellName { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Field { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Asset { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Country { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Operator { get; set; } = string.Empty;

    [Required]
    public string WellType { get; set; } = string.Empty; // "Onshore" or "Offshore"
}
