
namespace TradingGlossary.Application.GlossaryEntry.Model;

public class CreateGlossaryEntryDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public int GlossaryLetterId { get; set; }
    
    public ICollection<int>? GlossaryEntryTagIds { get; set; }
}