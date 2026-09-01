using FCW.Application.DTOs;
using FCW.Application.Exceptions;
using FCW.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCW.Api.Controllers;

[ApiController]
[Route("api/v1/wells/{wellId:int}/designs/{designId:int}/events")]
public class WellEventsController : ControllerBase
{
    private readonly IWellEventService _eventService;

    public WellEventsController(IWellEventService eventService)
    {
        _eventService = eventService;
    }

    // GET api/v1/wells/1/designs/1/events
    [HttpGet]
    public async Task<ActionResult<List<WellEventDto>>> GetAll(int wellId, int designId)
    {
        var events = await _eventService.GetByDesignIdAsync(wellId, designId);
        return Ok(events);
    }

    // GET api/v1/wells/1/designs/1/events/1
    [HttpGet("{eventId:int}")]
    public async Task<ActionResult<WellEventDto>> GetById(int wellId, int designId, int eventId)
    {
        var wellEvent = await _eventService.GetByIdAsync(wellId, designId, eventId);
        if (wellEvent is null)
            return NotFound(new { message = $"Event {eventId} not found for design {designId}." });

        return Ok(wellEvent);
    }

    // POST api/v1/wells/1/designs/1/events
    [HttpPost]
    public async Task<ActionResult<WellEventDto>> Create(int wellId, int designId, [FromBody] CreateWellEventDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var createdBy = "system";

        try
        {
            var created = await _eventService.CreateAsync(wellId, designId, dto, createdBy);
            if (created is null)
                return NotFound(new { message = $"Design {designId} not found for well {wellId}." });

            return CreatedAtAction(nameof(GetById), new { wellId, designId, eventId = created.Id }, created);
        }
        catch (InvalidWellEventException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE api/v1/wells/1/designs/1/events/1
    [HttpDelete("{eventId:int}")]
    public async Task<IActionResult> Delete(int wellId, int designId, int eventId)
    {
        var deleted = await _eventService.DeleteAsync(wellId, designId, eventId);
        if (!deleted)
            return NotFound(new { message = $"Event {eventId} not found for design {designId}." });

        return NoContent();
    }
}