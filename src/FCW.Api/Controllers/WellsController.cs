
using FCW.Application.DTOs;
using FCW.Application.Interfaces;
using global::FCW.Application.DTOs;
using global::FCW.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCW.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class WellsController : ControllerBase
{
    private readonly IWellService _wellService;

    public WellsController(IWellService wellService)
    {
        _wellService = wellService;
    }

    // GET api/v1/wells?search=abc&page=1&pageSize=20
    [HttpGet]
    public async Task<ActionResult<List<WellDto>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var wells = await _wellService.GetAllAsync(search, page, pageSize);
        return Ok(wells);
    }

    // GET api/v1/wells/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<WellDto>> GetById(int id)
    {
        var well = await _wellService.GetByIdAsync(id);
        if (well is null)
            return NotFound(new { message = $"Well with id {id} was not found." });

        return Ok(well);
    }

    // POST api/v1/wells
    [HttpPost]
    public async Task<ActionResult<WellDto>> Create([FromBody] CreateWellDto dto)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var createdBy = "system"; // replaced with real user identity in the Auth phase

        var created = await _wellService.CreateAsync(dto, createdBy);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}