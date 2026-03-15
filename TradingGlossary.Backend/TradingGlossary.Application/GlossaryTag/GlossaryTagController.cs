using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingGlossary.Application.GlossaryTag.Model;
using TradingGlossary.Application.GlossaryTag.Service.Interfaces;

namespace TradingGlossary.Application.GlossaryTag;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class GlossaryTagController : ControllerBase
{
    private readonly IGlossaryTagServiceRunner _glossaryTagServiceRunner;
    private readonly IGlossaryTagService _glossaryTagService;

    public GlossaryTagController(
        IGlossaryTagServiceRunner glossaryTagServiceRunner,
        IGlossaryTagService glossaryTagService)
    {
        _glossaryTagServiceRunner = glossaryTagServiceRunner;
        _glossaryTagService = glossaryTagService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<GlossaryTagDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGlossaryTags()
    {
        try
        {
            var glossaryTags = await _glossaryTagService.GetGlossaryTags();
            return Ok(glossaryTags);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(GlossaryTagDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateGlossaryTag([FromBody] CreateGlossaryTagDto request)
    {
        try
        {
            var created = await _glossaryTagServiceRunner.RunCreateGlossaryTag(request);
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

    [HttpPut]
    [ProducesResponseType(typeof(GlossaryTagDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateGlossaryTag([FromBody] UpdateGlossaryTagDto request)
    {
        try
        {
            var updated = await _glossaryTagServiceRunner.RunUpdateGlossaryTag(request);
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
    public async Task<IActionResult> DeleteGlossaryTag([FromRoute] int id)
    {
        try
        {
            var deleted = await _glossaryTagServiceRunner.RunDeleteGlossaryTag(id);
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