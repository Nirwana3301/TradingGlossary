using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingGlossary.Application.GlossaryLetter.Model;
using TradingGlossary.Application.GlossaryLetter.Service.Interfaces;

namespace TradingGlossary.Application.GlossaryLetter;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class GlossaryLetterController : ControllerBase
{
    private readonly IGlossaryLetterServiceRunner _glossaryLetterServiceRunner;
    private readonly IGlossaryLetterService _glossaryLetterService;

    public GlossaryLetterController(
        IGlossaryLetterServiceRunner glossaryLetterServiceRunner,
        IGlossaryLetterService glossaryLetterService)
    {
        _glossaryLetterServiceRunner = glossaryLetterServiceRunner;
        _glossaryLetterService = glossaryLetterService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<GlossaryLetterDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGlossaryLetters()
    {
        try
        {
            var glossaryLetters = await _glossaryLetterService.GetGlossaryLetters();
            return Ok(glossaryLetters);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GlossaryLetterDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGlossaryLetterById([FromRoute] int id)
    {
        try
        {
            var glossaryLetter = await _glossaryLetterService.GetGlossaryLetterById(id);
            return Ok(glossaryLetter);
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
    [ProducesResponseType(typeof(GlossaryLetterDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateGlossaryLetter([FromBody] CreateGlossaryLetterDto request)
    {
        try
        {
            var created = await _glossaryLetterServiceRunner.RunCreateGlossaryLetter(request);
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
    [ProducesResponseType(typeof(GlossaryLetterDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateGlossaryLetter([FromRoute] int id, [FromBody] UpdateGlossaryLetterDto request)
    {
        try
        {
            var updated = await _glossaryLetterServiceRunner.RunUpdateGlossaryLetter(id, request);
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
    public async Task<IActionResult> DeleteGlossaryLetter([FromRoute] int id)
    {
        try
        {
            var deleted = await _glossaryLetterServiceRunner.RunDeleteGlossaryLetter(id);
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