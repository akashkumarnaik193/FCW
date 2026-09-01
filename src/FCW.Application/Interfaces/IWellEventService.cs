using System;
using System.Collections.Generic;
using System.Text;

using FCW.Application.DTOs;

namespace FCW.Application.Interfaces;

public interface IWellEventService
{
    Task<List<WellEventDto>> GetByDesignIdAsync(int wellId, int designId);
    Task<WellEventDto?> GetByIdAsync(int wellId, int designId, int eventId);
    Task<WellEventDto?> CreateAsync(int wellId, int designId, CreateWellEventDto dto, string createdBy);
    Task<bool> DeleteAsync(int wellId, int designId, int eventId);
}
