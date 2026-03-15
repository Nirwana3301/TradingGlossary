using TradingGlossary.Database.Model.Extensions;

namespace TradingGlossary.Database.Model;

public class GlossaryEntry : EntityModelBase
{
    public int GlossaryLetterId { get; set; }
    public GlossaryLetter GlossaryLetter { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<GlossaryEntryTag> GlossaryEntryTags { get; set; } = new List<GlossaryEntryTag>();
}