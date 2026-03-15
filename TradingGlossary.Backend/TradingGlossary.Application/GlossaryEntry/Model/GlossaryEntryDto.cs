using TradingGlossary.Database.Model;

namespace TradingGlossary.Application.GlossaryEntry.Model;

public class GlossaryEntryDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}