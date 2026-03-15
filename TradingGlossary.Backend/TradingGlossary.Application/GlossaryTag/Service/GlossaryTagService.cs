using MarketingLvs.Application.Utils;
using Microsoft.EntityFrameworkCore;
using TradingGlossary.Application.GlossaryTag.Model;
using TradingGlossary.Application.GlossaryTag.Service.Interfaces;
using TradingGlossary.Application.Middlewares;
using TradingGlossary.Database.Database;

namespace TradingGlossary.Application.GlossaryTag;

public class GlossaryTagService : IGlossaryTagService
{
    private readonly TradingGlossaryDbContext _dbContext;
    private readonly SessionInfo _sessionInfo;
    private readonly IDateTimeService _dateTimeService;

    public GlossaryTagService(TradingGlossaryDbContext dbContext, SessionInfo sessionInfo,
        IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _sessionInfo = sessionInfo;
        _dateTimeService = dateTimeService;
    }

    public async Task<ICollection<GlossaryTagDto>> GetGlossaryTags()
    {
        var glossaryTags = await _dbContext.GlossaryTags
            .Select(gt => new GlossaryTagDto
            {
                Id = gt.Id,
                Label = gt.Label,
                SortOrder = gt.SortOrder
            }).ToListAsync(_sessionInfo.CancellationToken);

        if (glossaryTags == null)
        {
            throw new Exception("GlossaryTags konnten nicht gefunden werden");
        }

        return glossaryTags;
    }

    public GlossaryTagDto CreateGlossaryTag(CreateGlossaryTagDto glossaryTagCreateDto)
    {
        var glossaryTag = new Database.Model.GlossaryTag
        {
            Label = glossaryTagCreateDto.Label,
            SortOrder = glossaryTagCreateDto.SortOrder,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = _sessionInfo.ClerkUser
        };

        _dbContext.GlossaryTags.Add(glossaryTag);

        return new GlossaryTagDto
        {
            Id = glossaryTag.Id,
            Label = glossaryTag.Label,
            SortOrder = glossaryTag.SortOrder,
        };
    }

    public async Task<GlossaryTagDto> UpdateGlossaryTag(UpdateGlossaryTagDto glossaryTagUpdateDto)
    {
        var tagToUpdate = await _dbContext.GlossaryTags.FirstOrDefaultAsync(gt => gt.Id == glossaryTagUpdateDto.Id);

        if (tagToUpdate == null)
        {
            throw new Exception($"GlossaryTag mit Id {glossaryTagUpdateDto.Id} konnte nicht gefunden werden");
        }

        tagToUpdate.Label = glossaryTagUpdateDto.Label;
        tagToUpdate.SortOrder = glossaryTagUpdateDto.SortOrder;
        tagToUpdate.ModifiedAt = _dateTimeService.Now;
        tagToUpdate.ModifiedBy = _sessionInfo.ClerkUser;

        return new GlossaryTagDto
        {
            Id = tagToUpdate.Id,
            Label = tagToUpdate.Label,
            SortOrder = tagToUpdate.SortOrder
        };
    }

    public async Task<bool> DeleteGlossaryTag(int id)
    {
        var tagToDelete = await _dbContext.GlossaryTags.FirstOrDefaultAsync(gt => gt.Id == id);

        if (tagToDelete == null)
        {
            throw new Exception($"GlossaryTag mit Id {id} konnte nicht gefunden werden");
        }

        _dbContext.GlossaryTags.Remove(tagToDelete);
        return true;
    }
}