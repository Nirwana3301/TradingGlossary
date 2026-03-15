namespace TradingGlossary.Application.GlossaryLetter.Model;

public class CreateGlossaryLetterDto
{
    public string Code { get; set; }
    public string Label { get; set; }
    public int SortOrder { get; set; }
}