using TradingGlossary.Database.Model.Extensions;

namespace TradingGlossary.Database.Model;

public class Role : EntityModelBase
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    
    public ICollection<User> Users { get; set; } = new List<User>();
}