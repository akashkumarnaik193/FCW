using System;
using System.Collections.Generic;
using System.Text;

using FCW.Application.DTOs;

namespace FCW.Application.Interfaces;

public interface IWellService
{
    Task<List<WellDto>> GetAllAsync(string? search, int page, int pageSize);
    Task<WellDto?> GetByIdAsync(int id);
    Task<WellDto> CreateAsync(CreateWellDto dto, string createdBy);
}
