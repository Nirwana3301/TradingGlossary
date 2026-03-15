using TradingGlossary.Application.GlossaryEntry.Model;

namespace TradingGlossary.Application.GlossaryEntry.Service.Interfaces;

public interface IGlossaryEntryService
{
    Task<ICollection<GlossaryEntryDto>> GetGlossaryEntries();
    
    Task<GlossaryEntryDto> GetGlossaryEntryById(int id);
    
    GlossaryEntryDto CreateGlossaryEntry(CreateGlossaryEntryDto createGlossaryEntryDto);
    
    Task<GlossaryEntryDto> UpdateGlossaryEntry(int id, UpdateGlossaryEntryDto updateGlossaryEntryDto);
    
    Task<bool> DeleteGlossaryEntry(int id);
}