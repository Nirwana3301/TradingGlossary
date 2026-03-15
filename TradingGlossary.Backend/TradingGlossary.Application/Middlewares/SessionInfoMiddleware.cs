using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TradingGlossary.Application.Middlewares;

public class SessionInfoMiddleware
{
	private readonly RequestDelegate _next;

	public SessionInfoMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		if (context.Request.Path.ToString().StartsWith("/api"))
		{
			var sessionInfo = context.RequestServices.GetService<SessionInfo>() ??
			                  throw new Exception("ISessionInfo konnte nicht gefunden werden");

			string clerkUser = UserHelper.GetClerkUser(context.User, context.Request.Headers);
			var cancellationToken = context.RequestAborted;

			sessionInfo.SetInfo(clerkUser, cancellationToken);
		}
		await _next(context);
	}
}