namespace TradingGlossary.Application.User.Model;

public class UpdateUserDto
{
    public string ClerkUserId { get; set; } = null!;

    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PrimaryEmail { get; set; }
}