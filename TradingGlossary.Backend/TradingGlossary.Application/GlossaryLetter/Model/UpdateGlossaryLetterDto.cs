namespace TradingGlossary.Application.GlossaryLetter.Model;

public class UpdateGlossaryLetterDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Label { get; set; }
    public int SortOrder { get; set; }

    public ICollection<Database.Model.GlossaryEntry> GlossaryEntries { get; set; }
}