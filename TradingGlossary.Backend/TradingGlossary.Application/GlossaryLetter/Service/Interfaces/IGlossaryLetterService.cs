using TradingGlossary.Application.GlossaryLetter.Model;

namespace TradingGlossary.Application.GlossaryLetter.Service.Interfaces;

public interface IGlossaryLetterService
{
    Task<ICollection<GlossaryLetterDto>> GetGlossaryLetters();
    
    Task<GlossaryLetterDto> GetGlossaryLetterById(int id);
    
    GlossaryLetterDto CreateGlossaryLetter(CreateGlossaryLetterDto glossaryLetterCreateDto);
    
    Task<GlossaryLetterDto> UpdateGlossaryLetter(int id, UpdateGlossaryLetterDto glossaryLetterUpdateDto);
    
    Task<bool> DeleteGlossaryLetter(int id);
}