using TradingGlossary.Application.GlossaryEntry.Model;

namespace TradingGlossary.Application.GlossaryEntry.Service.Interfaces;

public interface IGlossaryEntryService
{
    Task<ICollection<GlossaryEntryDto>> GetGlossaryEntries();
    
    Task<GlossaryEntryDto> GetGlossaryEntryById(int id);
    
    Task<List<GlossaryEntryDto>> GetGlossaryEntriesByLetterId(int letterId);
    
    GlossaryEntryDto CreateGlossaryEntry(CreateGlossaryEntryDto createGlossaryEntryDto);
    
    Task<GlossaryEntryDto> UpdateGlossaryEntry(int id, UpdateGlossaryEntryDto updateGlossaryEntryDto);
    
    Task<bool> DeleteGlossaryEntry(int id);
}