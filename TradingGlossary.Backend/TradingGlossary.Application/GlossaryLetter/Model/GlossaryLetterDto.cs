namespace TradingGlossary.Application.GlossaryLetter.Model;

public class GlossaryLetterDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Label { get; set; }
    public int SortOrder { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }

    public ICollection<Database.Model.GlossaryEntry> GlossaryEntries { get; set; }
}