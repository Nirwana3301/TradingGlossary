namespace TradingGlossary.Application.Middlewares;

public class SessionInfo 
{
	public string ClerkUser { get; private set; } = string.Empty;
	public CancellationToken CancellationToken { get; private set; }
	
	public void SetInfo(string clerkUser, CancellationToken cancellationToken = default)
	{
		ClerkUser = clerkUser;
		CancellationToken = cancellationToken;
	}
}