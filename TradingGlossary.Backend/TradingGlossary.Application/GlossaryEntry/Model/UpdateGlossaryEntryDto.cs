namespace TradingGlossary.Application.GlossaryEntry.Model;

public class UpdateGlossaryEntryDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public int GlossaryLetterId { get; set; }
    
    public ICollection<int>? GlossaryEntryTagIds { get; set; }
}