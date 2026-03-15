using TradingGlossary.Application.GlossaryTag.Model;
using TradingGlossary.Application.GlossaryTag.Service.Interfaces;
using TradingGlossary.Application.User.Service.Interfaces;
using TradingGlossary.Database.Database;

namespace TradingGlossary.Application.GlossaryTag.Service;

public class GlossaryTagServiceRunner : IGlossaryTagServiceRunner
{
    private readonly TradingGlossaryDbContext _dbContext;
    private readonly IGlossaryTagService _service;

    public GlossaryTagServiceRunner(IGlossaryTagService service, TradingGlossaryDbContext dbContext)
    {
        _service = service;
        _dbContext = dbContext;
    }

    public async Task<GlossaryTagDto> RunCreateGlossaryTag(CreateGlossaryTagDto glossaryTagCreateDto)
    {
        var newTag = _service.CreateGlossaryTag(glossaryTagCreateDto);
        await _dbContext.SaveChangesAsync();
        return newTag;
    }

    public async Task<GlossaryTagDto> RunUpdateGlossaryTag(UpdateGlossaryTagDto glossaryTagUpdateDto)
    {
        var updatedTag = await _service.UpdateGlossaryTag(glossaryTagUpdateDto);
        await _dbContext.SaveChangesAsync();
        return updatedTag;
    }

    public async Task<bool> RunDeleteGlossaryTag(int id)
    {
        var result = await _service.DeleteGlossaryTag(id);
        await _dbContext.SaveChangesAsync();
        return result;
    }
}