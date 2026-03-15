using TradingGlossary.Application.GlossaryEntry.Model;
using TradingGlossary.Application.GlossaryEntry.Service.Interfaces;
using TradingGlossary.Database.Database;

namespace TradingGlossary.Application.GlossaryEntry.Service;

public class GlossaryEntryServiceRunner : IGlossaryEntryServiceRunner
{
    private readonly TradingGlossaryDbContext _dbContext;
    private readonly IGlossaryEntryService _glossaryEntryService;

    public GlossaryEntryServiceRunner(TradingGlossaryDbContext dbContext, IGlossaryEntryService glossaryEntryService)
    {
        _dbContext = dbContext;
        _glossaryEntryService = glossaryEntryService;
    }

    public async Task<GlossaryEntryDto> RunCreateGlossaryEntry(CreateGlossaryEntryDto createGlossaryEntryDto)
    {
        var glossaryEntry = _glossaryEntryService.CreateGlossaryEntry(createGlossaryEntryDto);
        await _dbContext.SaveChangesAsync();
        return glossaryEntry;
    }

    public async Task<GlossaryEntryDto> RunUpdateGlossaryEntry(int id, UpdateGlossaryEntryDto updateGlossaryEntryDto)
    {
       var updatedGlossaryEntry = await _glossaryEntryService.UpdateGlossaryEntry(id, updateGlossaryEntryDto);
       await _dbContext.SaveChangesAsync();
       return updatedGlossaryEntry;
    }

    public async Task<bool> RunDeleteGlossaryEntry(int id)
    {
        var result = await _glossaryEntryService.DeleteGlossaryEntry(id);
        await _dbContext.SaveChangesAsync();
        return result;
    }
}