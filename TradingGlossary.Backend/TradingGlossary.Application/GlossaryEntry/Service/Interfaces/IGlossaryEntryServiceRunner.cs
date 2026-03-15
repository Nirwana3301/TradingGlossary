using TradingGlossary.Application.GlossaryEntry.Model;

namespace TradingGlossary.Application.GlossaryEntry.Service.Interfaces;

public interface IGlossaryEntryServiceRunner
{
    Task<GlossaryEntryDto> RunCreateGlossaryEntry(CreateGlossaryEntryDto createGlossaryEntryDto);
    
    Task<GlossaryEntryDto> RunUpdateGlossaryEntry(int id, UpdateGlossaryEntryDto updateGlossaryEntryDto);
    
    Task<bool> RunDeleteGlossaryEntry(int id);
}