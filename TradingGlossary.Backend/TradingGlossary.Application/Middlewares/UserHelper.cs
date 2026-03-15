using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TradingGlossary.Application.Middlewares;

public class UserHelper
{
	public static string GetClerkUser(ClaimsPrincipal user, IHeaderDictionary headerDictionary)
	{
		if (user is null or { Identity: null })
			return "";

		var claimsIdentity = user.Identity as ClaimsIdentity;
			
		string? name = user.Identity.Name;
		
		name ??= (claimsIdentity?.Claims!).FirstOrDefault(x => x.Type == "unique_name")?.Value;
		name ??= (claimsIdentity?.Claims!).FirstOrDefault(x => x.Type == "unique_name2")?.Value;
			
		name ??= (claimsIdentity?.Claims!).FirstOrDefault(x => x.Type == "username")?.Value;
		name ??= (claimsIdentity?.Claims!).FirstOrDefault(x => x.Type == "username2")?.Value;
		name ??= (claimsIdentity?.Claims!).FirstOrDefault(x => x.Type == "name2")?.Value;
		name ??= (claimsIdentity?.Claims!).FirstOrDefault(x => x.Type == "name")?.Value;
		name ??= (claimsIdentity?.Claims!).FirstOrDefault(x => x.Type == "client_id")?.Value;
		name ??= "";

		// @pending entfernen
		int ipos = name.LastIndexOf('@');
		if (ipos >= 0)
		{
			name = name.Substring(0, ipos);
		}

		// Pending\ entfernen
		if (name.StartsWith("PENDING\\"))
		{
			name = name.Substring(8);
		}
		
		return name;
	}
}