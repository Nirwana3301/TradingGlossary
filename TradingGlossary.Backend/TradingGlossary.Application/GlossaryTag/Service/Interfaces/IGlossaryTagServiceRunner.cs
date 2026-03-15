using TradingGlossary.Application.GlossaryTag.Model;

namespace TradingGlossary.Application.GlossaryTag.Service.Interfaces;

public interface IGlossaryTagServiceRunner
{
    Task<GlossaryTagDto> RunCreateGlossaryTag(CreateGlossaryTagDto glossaryTagCreateDto);

    Task<GlossaryTagDto> RunUpdateGlossaryTag(UpdateGlossaryTagDto glossaryTagUpdateDto);

    Task<bool> RunDeleteGlossaryTag(int id);
}