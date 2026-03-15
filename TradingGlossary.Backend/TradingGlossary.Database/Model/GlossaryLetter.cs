using TradingGlossary.Database.Model.Extensions;

namespace TradingGlossary.Database.Model;

public class GlossaryLetter : EntityModelBase
{
    public string Code { get; set; } = null!;
    public string Label { get; set; } = null!;
    public int SortOrder { get; set; }

    public ICollection<GlossaryEntry> GlossaryEntries { get; set; } = new List<GlossaryEntry>();
}