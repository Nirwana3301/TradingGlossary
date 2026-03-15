using TradingGlossary.Database.Model.Extensions;

namespace TradingGlossary.Database.Model;

public class GlossaryTag : EntityModelBase
{
    public string Label { get; set; } = null!; 
    public int SortOrder { get; set; }

    public ICollection<GlossaryEntryTag> GlossaryEntryTags { get; set; } = new List<GlossaryEntryTag>();
}