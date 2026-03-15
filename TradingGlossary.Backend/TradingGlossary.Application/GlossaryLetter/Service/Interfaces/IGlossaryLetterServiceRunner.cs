using TradingGlossary.Application.GlossaryLetter.Model;

namespace TradingGlossary.Application.GlossaryLetter.Service.Interfaces;

public interface IGlossaryLetterServiceRunner
{
    Task<GlossaryLetterDto> RunCreateGlossaryLetter(CreateGlossaryLetterDto glossaryLetterCreateDto);
    
    Task<GlossaryLetterDto> RunUpdateGlossaryLetter(int id, UpdateGlossaryLetterDto glossaryLetterUpdateDto);
    
    Task<bool> RunDeleteGlossaryLetter(int id);
}