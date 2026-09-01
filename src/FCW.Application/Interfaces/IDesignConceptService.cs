using System;
using System.Collections.Generic;
using System.Text;

using FCW.Application.DTOs;

namespace FCW.Application.Interfaces;

public interface IDesignConceptService
{
    Task<List<DesignConceptDto>> GetByWellIdAsync(int wellId);
    Task<DesignConceptDto?> GetByIdAsync(int wellId, int designId);
    Task<DesignConceptDto?> CreateAsync(int wellId, CreateDesignConceptDto dto, string createdBy);
    Task<DesignConceptDto?> UpdateAsync(int wellId, int designId, UpdateDesignConceptDto dto, string modifiedBy);
    Task<bool> DeleteAsync(int wellId, int designId);
}
