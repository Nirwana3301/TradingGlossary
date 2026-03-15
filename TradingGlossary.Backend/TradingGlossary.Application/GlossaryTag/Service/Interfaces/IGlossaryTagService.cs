using TradingGlossary.Application.GlossaryTag.Model;

namespace TradingGlossary.Application.GlossaryTag.Service.Interfaces;

public interface IGlossaryTagService
{
    Task<ICollection<GlossaryTagDto>> GetGlossaryTags();

    GlossaryTagDto CreateGlossaryTag(CreateGlossaryTagDto glossaryTagCreateDto);

    Task<GlossaryTagDto> UpdateGlossaryTag(UpdateGlossaryTagDto glossaryTagUpdateDto);

    Task<bool> DeleteGlossaryTag(int id);
}