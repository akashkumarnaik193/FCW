using FCW.Application.DTOs;
using FCW.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCW.Api.Controllers;

[ApiController]
[Route("api/v1/wells/{wellId:int}/designs")]
public class DesignConceptsController : ControllerBase
{
    private readonly IDesignConceptService _designService;

    public DesignConceptsController(IDesignConceptService designService)
    {
        _designService = designService;
    }

    // GET api/v1/wells/5/designs
    [HttpGet]
    public async Task<ActionResult<List<DesignConceptDto>>> GetAll(int wellId)
    {
        var designs = await _designService.GetByWellIdAsync(wellId);
        return Ok(designs);
    }

    // GET api/v1/wells/5/designs/3
    [HttpGet("{designId:int}")]
    public async Task<ActionResult<DesignConceptDto>> GetById(int wellId, int designId)
    {
        var design = await _designService.GetByIdAsync(wellId, designId);
        if (design is null)
            return NotFound(new { message = $"Design {designId} not found for well {wellId}." });

        return Ok(design);
    }

    // POST api/v1/wells/5/designs
    [HttpPost]
    public async Task<ActionResult<DesignConceptDto>> Create(int wellId, [FromBody] CreateDesignConceptDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var createdBy = "system"; // replaced with real user identity in the Auth phase

        var created = await _designService.CreateAsync(wellId, dto, createdBy);
        if (created is null)
            return NotFound(new { message = $"Well {wellId} not found." });

        return CreatedAtAction(nameof(GetById), new { wellId, designId = created.Id }, created);
    }

    // PUT api/v1/wells/5/designs/3
    [HttpPut("{designId:int}")]
    public async Task<ActionResult<DesignConceptDto>> Update(int wellId, int designId, [FromBody] UpdateDesignConceptDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var modifiedBy = "system";

        var updated = await _designService.UpdateAsync(wellId, designId, dto, modifiedBy);
        if (updated is null)
            return NotFound(new { message = $"Design {designId} not found for well {wellId}." });

        return Ok(updated);
    }

    // DELETE api/v1/wells/5/designs/3
    [HttpDelete("{designId:int}")]
    public async Task<IActionResult> Delete(int wellId, int designId)
    {
        var deleted = await _designService.DeleteAsync(wellId, designId);
        if (!deleted)
            return NotFound(new { message = $"Design {designId} not found for well {wellId}." });

        return NoContent();
    }
}