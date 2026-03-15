using TradingGlossary.Database.Model.Extensions;

namespace TradingGlossary.Database.Model;

public class User : EntityModelBase 
{
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    
    public string ClerkUserId { get; set; } = null!;

    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PrimaryEmail { get; set; }
}