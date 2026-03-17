using MarketingLvs.Application.Utils;
using Microsoft.EntityFrameworkCore;
using TradingGlossary.Application.GlossaryEntry.Model;
using TradingGlossary.Application.GlossaryEntry.Service.Interfaces;
using TradingGlossary.Application.Middlewares;
using TradingGlossary.Database.Database;

namespace TradingGlossary.Application.GlossaryEntry;

public class GlossaryEntryService : IGlossaryEntryService
{
    private readonly TradingGlossaryDbContext _dbContext;
    private readonly SessionInfo _sessionInfo;
    private readonly IDateTimeService _dateTimeService;

    public GlossaryEntryService(TradingGlossaryDbContext dbContext, SessionInfo sessionInfo,
        IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _sessionInfo = sessionInfo;
        _dateTimeService = dateTimeService;
    }

    public async Task<ICollection<GlossaryEntryDto>> GetGlossaryEntries()
    {
        var glossaryEntries = await _dbContext.GlossaryEntries
            .Select(g => new GlossaryEntryDto
            {
                Title = g.Title,
                Description = g.Description,
                CreatedAt = g.CreatedAt,
                CreatedBy = g.CreatedBy,
                ModifiedAt = g.ModifiedAt,
                ModifiedBy = g.ModifiedBy
            }).ToListAsync(_sessionInfo.CancellationToken);

        return glossaryEntries;
    }

    public async Task<GlossaryEntryDto> GetGlossaryEntryById(int id)
    {
        var glossaryEntry = await _dbContext.GlossaryEntries
            .Where(g => g.Id == id)
            .Select(g => new GlossaryEntryDto
            {
                Title = g.Title,
                Description = g.Description,
                CreatedAt = g.CreatedAt,
                CreatedBy = g.CreatedBy,
                ModifiedAt = g.ModifiedAt,
                ModifiedBy = g.ModifiedBy
            }).FirstOrDefaultAsync(_sessionInfo.CancellationToken);

        if (glossaryEntry == null)
        {
            throw new Exception($"Glossary entry with id {id} not found.");
        }

        return glossaryEntry;
    }

    public async Task<List<GlossaryEntryDto>> GetGlossaryEntriesByLetterId(int letterId)
    {
        var glossaryEntries = await _dbContext.GlossaryEntries
            .Where(g => g.GlossaryLetterId == letterId)
            .Select(g => new GlossaryEntryDto
            {
                Title = g.Title,
                Description = g.Description,
                CreatedAt = g.CreatedAt,
                CreatedBy = g.CreatedBy,
                ModifiedAt = g.ModifiedAt,
                ModifiedBy = g.ModifiedBy
            }).ToListAsync(_sessionInfo.CancellationToken);

        return glossaryEntries;
    }

    public GlossaryEntryDto CreateGlossaryEntry(CreateGlossaryEntryDto createGlossaryEntryDto)
    {
        var glossaryEntry = new Database.Model.GlossaryEntry
        {
            Title = createGlossaryEntryDto.Title,
            Description = createGlossaryEntryDto.Description,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = _sessionInfo.ClerkUser
        };

        _dbContext.GlossaryEntries.Add(glossaryEntry);

        return new GlossaryEntryDto
        {
            Title = glossaryEntry.Title,
            Description = glossaryEntry.Description,
            CreatedAt = glossaryEntry.CreatedAt,
            CreatedBy = glossaryEntry.CreatedBy
        };
    }

    public async Task<GlossaryEntryDto> UpdateGlossaryEntry(int id, UpdateGlossaryEntryDto updateGlossaryEntryDto)
    {
        var glossaryEntry = await _dbContext.GlossaryEntries
            .Where(g => g.Id == id)
            .FirstOrDefaultAsync(_sessionInfo.CancellationToken);

        if (glossaryEntry == null)
        {
            throw new Exception($"Glossary entry with id {id} not found.");
        }

        glossaryEntry.Title = updateGlossaryEntryDto.Title;
        glossaryEntry.Description = updateGlossaryEntryDto.Description;
        glossaryEntry.ModifiedAt = _dateTimeService.Now;
        glossaryEntry.ModifiedBy = _sessionInfo.ClerkUser;

        return new GlossaryEntryDto
        {
            Title = glossaryEntry.Title,
            Description = glossaryEntry.Description,
            CreatedAt = glossaryEntry.CreatedAt,
            CreatedBy = glossaryEntry.CreatedBy,
            ModifiedAt = glossaryEntry.ModifiedAt,
            ModifiedBy = glossaryEntry.ModifiedBy
        };
    }

    public async Task<bool> DeleteGlossaryEntry(int id)
    {
        var glossaryEntry = await _dbContext.GlossaryEntries
            .Where(g => g.Id == id)
            .FirstOrDefaultAsync(_sessionInfo.CancellationToken);

        if (glossaryEntry == null)
        {
            return false;
        }

        _dbContext.GlossaryEntries.Remove(glossaryEntry);
        return true;
    }
}