namespace TradingGlossary.Database.Model;

public class GlossaryEntryTag
{
    public int GlossaryEntryId { get; set; }
    public int GlossaryTagId { get; set; }
    
    public GlossaryEntry GlossaryEntry { get; set; } = null!;
    public GlossaryTag GlossaryTag { get; set; } = null!;
}