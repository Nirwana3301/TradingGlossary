namespace TradingGlossary.Database.Model.Extensions;

public abstract class EntityModelBase
{
    public int Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}
