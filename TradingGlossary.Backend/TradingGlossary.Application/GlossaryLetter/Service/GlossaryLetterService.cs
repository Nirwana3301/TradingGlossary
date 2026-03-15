using MarketingLvs.Application.Utils;
using Microsoft.EntityFrameworkCore;
using TradingGlossary.Application.GlossaryLetter.Model;
using TradingGlossary.Application.GlossaryLetter.Service.Interfaces;
using TradingGlossary.Application.Middlewares;
using TradingGlossary.Database.Database;

namespace TradingGlossary.Application.GlossaryLetter.Service;

public class GlossaryLetterService : IGlossaryLetterService
{
    private readonly TradingGlossaryDbContext _dbContext;
    private readonly SessionInfo _sessionInfo;
    private readonly IDateTimeService _dateTimeService;

    public GlossaryLetterService(TradingGlossaryDbContext dbContext, SessionInfo sessionInfo,
        IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _sessionInfo = sessionInfo;
        _dateTimeService = dateTimeService;
    }

    public async Task<ICollection<GlossaryLetterDto>> GetGlossaryLetters()
    {
        var glossaryLetters = await _dbContext.GlossaryLetters
            .Select(gl => new GlossaryLetterDto
            {
                Id = gl.Id,
                Code = gl.Code,
                Label = gl.Label,
                SortOrder = gl.SortOrder,
                CreatedAt = gl.CreatedAt,
                CreatedBy = gl.CreatedBy,
                ModifiedAt = gl.ModifiedAt,
                ModifiedBy = gl.ModifiedBy
            }).ToListAsync(_sessionInfo.CancellationToken);
        
        return glossaryLetters;
    }

    public async Task<GlossaryLetterDto> GetGlossaryLetterById(int id)
    {
        var glossaryLetter = await _dbContext.GlossaryLetters
            .Where(gl => gl.Id == id)
            .Select(gl => new GlossaryLetterDto
            {
                Id = gl.Id,
                Code = gl.Code,
                Label = gl.Label,
                SortOrder = gl.SortOrder,
                CreatedAt = gl.CreatedAt,
                CreatedBy = gl.CreatedBy,
                ModifiedAt = gl.ModifiedAt,
                ModifiedBy = gl.ModifiedBy
            }).FirstOrDefaultAsync(_sessionInfo.CancellationToken);

        if (glossaryLetter == null)
        {
            throw new Exception($"GlossaryLetter with id {id} not found.");
        }

        return glossaryLetter;
    }

    public GlossaryLetterDto CreateGlossaryLetter(CreateGlossaryLetterDto glossaryLetterCreateDto)
    {
        var glossaryLetterEntity = new Database.Model.GlossaryLetter
        {
            Code = glossaryLetterCreateDto.Code,
            Label = glossaryLetterCreateDto.Label,
            SortOrder = glossaryLetterCreateDto.SortOrder,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = _sessionInfo.ClerkUser
        };

        _dbContext.GlossaryLetters.Add(glossaryLetterEntity);

        return new GlossaryLetterDto
        {
            Id = glossaryLetterEntity.Id,
            Code = glossaryLetterEntity.Code,
            Label = glossaryLetterEntity.Label,
            SortOrder = glossaryLetterEntity.SortOrder,
            CreatedAt = glossaryLetterEntity.CreatedAt,
            CreatedBy = glossaryLetterEntity.CreatedBy
        };
    }

    public async Task<GlossaryLetterDto> UpdateGlossaryLetter(int id, UpdateGlossaryLetterDto glossaryLetterUpdateDto)
    {
        var glossaryLetterEntity =
            await _dbContext.GlossaryLetters.FirstOrDefaultAsync(fod => fod.Id == id, _sessionInfo.CancellationToken);

        if (glossaryLetterEntity == null)
        {
            throw new Exception($"GlossaryLetter with id {id} not found.");
        }

        glossaryLetterEntity.Code = glossaryLetterUpdateDto.Code;
        glossaryLetterEntity.Label = glossaryLetterUpdateDto.Label;
        glossaryLetterEntity.SortOrder = glossaryLetterUpdateDto.SortOrder;
        glossaryLetterEntity.ModifiedAt = _dateTimeService.Now;
        glossaryLetterEntity.ModifiedBy = _sessionInfo.ClerkUser;

        return new GlossaryLetterDto
        {
            Id = glossaryLetterEntity.Id,
            Code = glossaryLetterEntity.Code,
            Label = glossaryLetterEntity.Label,
            SortOrder = glossaryLetterEntity.SortOrder,
            CreatedAt = glossaryLetterEntity.CreatedAt,
            CreatedBy = glossaryLetterEntity.CreatedBy,
            ModifiedAt = glossaryLetterEntity.ModifiedAt,
            ModifiedBy = glossaryLetterEntity.ModifiedBy
        };
    }

    public async Task<bool> DeleteGlossaryLetter(int id)
    {
        var glossaryLetterEntity =
            await _dbContext.GlossaryLetters.FirstOrDefaultAsync(fod => fod.Id == id, _sessionInfo.CancellationToken);

        if (glossaryLetterEntity == null)
        {
            throw new Exception($"GlossaryLetter with id {id} not found.");
        }

        _dbContext.GlossaryLetters.Remove(glossaryLetterEntity);
        return true;
    }
}