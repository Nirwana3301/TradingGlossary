using TradingGlossary.Application.GlossaryLetter.Model;
using TradingGlossary.Application.GlossaryLetter.Service.Interfaces;
using TradingGlossary.Database.Database;

namespace TradingGlossary.Application.GlossaryLetter.Service;

public class GlossaryLetterServiceRunner : IGlossaryLetterServiceRunner
{
    private readonly TradingGlossaryDbContext _dbContext;
    private readonly IGlossaryLetterService _glossaryLetterService;

    public GlossaryLetterServiceRunner(TradingGlossaryDbContext dbContext, IGlossaryLetterService glossaryLetterService)
    {
        _dbContext = dbContext;
        _glossaryLetterService = glossaryLetterService;
    }

    public async Task<GlossaryLetterDto> RunCreateGlossaryLetter(CreateGlossaryLetterDto glossaryLetterCreateDto)
    {
        var newUser = _glossaryLetterService.CreateGlossaryLetter(glossaryLetterCreateDto);
        await _dbContext.SaveChangesAsync();
        return newUser;
    }

    public async Task<GlossaryLetterDto> RunUpdateGlossaryLetter(int id, UpdateGlossaryLetterDto glossaryLetterUpdateDto)
    {
        await _glossaryLetterService.UpdateGlossaryLetter(id, glossaryLetterUpdateDto);
        await _dbContext.SaveChangesAsync();
        return await _glossaryLetterService.GetGlossaryLetterById(id);
    }

    public async Task<bool> RunDeleteGlossaryLetter(int id)
    {
        var result = await _glossaryLetterService.DeleteGlossaryLetter(id);
        await _dbContext.SaveChangesAsync();
        return result;
    }
}