using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingGlossary.Application.GlossaryEntry.Model;
using TradingGlossary.Application.GlossaryEntry.Service.Interfaces;

namespace TradingGlossary.Application.GlossaryEntry;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class GlossaryEntryController : ControllerBase
{
    private readonly IGlossaryEntryServiceRunner _glossaryEntryServiceRunner;
    private readonly IGlossaryEntryService _glossaryEntryService;

    public GlossaryEntryController(IGlossaryEntryServiceRunner glossaryEntryServiceRunner,
        IGlossaryEntryService glossaryEntryService)
    {
        _glossaryEntryServiceRunner = glossaryEntryServiceRunner;
        _glossaryEntryService = glossaryEntryService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<GlossaryEntryDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGlossaryEntries()
    {
        try
        {
            var glossaryEntries = await _glossaryEntryService.GetGlossaryEntries();
            return Ok(glossaryEntries);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GlossaryEntryDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGlossaryEntryById([FromRoute] int id)
    {
        try
        {
            var transactionType = await _glossaryEntryService.GetGlossaryEntryById(id);
            return Ok(transactionType);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("letter/{letterId:int}")]
    [ProducesResponseType(typeof(List<GlossaryEntryDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGlossaryEntriesByLetterId([FromRoute] int letterId)
    {
        try
        {
            var glossaryEntries = await _glossaryEntryService.GetGlossaryEntriesByLetterId(letterId);
            return Ok(glossaryEntries);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(GlossaryEntryDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateGlossaryEntry([FromBody] CreateGlossaryEntryDto request)
    {
        try
        {
            var created = await _glossaryEntryServiceRunner.RunCreateGlossaryEntry(request);
            return Ok(created);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(GlossaryEntryDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateGlossaryEntry([FromRoute] int id, [FromBody] UpdateGlossaryEntryDto request)
    {
        try
        {
            var updated = await _glossaryEntryServiceRunner.RunUpdateGlossaryEntry(id, request);
            return Ok(updated);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteGlossaryEntry([FromRoute] int id)
    {
        try
        {
            var deleted = await _glossaryEntryServiceRunner.RunDeleteGlossaryEntry(id);
            return Ok(deleted);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}
