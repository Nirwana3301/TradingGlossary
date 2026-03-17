using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using MarketingLvs.Application.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TradingGlossary.API;
using TradingGlossary.Application.GlossaryEntry;
using TradingGlossary.Application.GlossaryEntry.Service;
using TradingGlossary.Application.GlossaryEntry.Service.Interfaces;
using TradingGlossary.Application.GlossaryLetter.Service;
using TradingGlossary.Application.GlossaryLetter.Service.Interfaces;
using TradingGlossary.Application.GlossaryTag;
using TradingGlossary.Application.GlossaryTag.Service;
using TradingGlossary.Application.GlossaryTag.Service.Interfaces;
using TradingGlossary.Application.Middlewares;
using TradingGlossary.Application.User.Service;
using TradingGlossary.Application.User.Service.Interfaces;
using TradingGlossary.Database.Database;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
	#if DEBUG
		EnvironmentName = Environments.Development
	#endif
});

// ### Services konfigurieren ###
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();

// Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserServiceRunner, UserServiceRunner>();

builder.Services.AddScoped<IGlossaryLetterService, GlossaryLetterService>();
builder.Services.AddScoped<IGlossaryLetterServiceRunner, GlossaryLetterServiceRunner>();

builder.Services.AddScoped<IGlossaryTagService, GlossaryTagService>();
builder.Services.AddScoped<IGlossaryTagServiceRunner, GlossaryTagServiceRunner>();

builder.Services.AddScoped<IGlossaryEntryService, GlossaryEntryService>();
builder.Services.AddScoped<IGlossaryEntryServiceRunner, GlossaryEntryServiceRunner>();

builder.Services.AddScoped<SessionInfo>();
builder.Services.AddScoped<IDateTimeService, DateTimeService>();

// DbContext
builder.Services.AddDbContext<TradingGlossaryDbContext>(options =>
{
	options.UseNpgsql(
		builder.Configuration.GetConnectionString("DefaultConnection"),
		b => b.MigrationsAssembly("TradingGlossary.Database")
	);
	if (builder.Environment.IsDevelopment())
	{
		options.EnableDetailedErrors();
		options.EnableSensitiveDataLogging();
	}
});

// CORS
builder.Services.AddCors();

// Add Swagger mit deiner vollständigen Konfiguration
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "TradingGlossary API", Version = "v1" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."
	});
	c.AddSecurityRequirement(x =>
	{
		var requirement = new OpenApiSecurityRequirement
		{
			{
				new OpenApiSecuritySchemeReference("Bearer"), []
			}
		};
		return requirement;
	});
	
	c.EnableAnnotations();
	c.CustomOperationIds(apiDesc =>
	{
		var result = apiDesc.TryGetMethodInfo(out MethodInfo methodInfo);
		if (!result)
			return null;
		var swaggerOperation = methodInfo.CustomAttributes
			.Where(w => w.AttributeType.Name == "SwaggerOperationAttribute")
			.FirstOrDefault();
		if (swaggerOperation != null)
		{
			var memberName = swaggerOperation.NamedArguments.FirstOrDefault()!.TypedValue.Value!.ToString();
			return memberName;
		}

		return methodInfo.Name;
	});
	c.ResolveConflictingActions(api => api.First());
	c.SchemaFilter<ForceAllPropertiesRequiredAndRespectNullabilitySchemaFilter>();
});


// JWT Authentication
var authAuthority = builder.Configuration["Authentication:Authority"] ?? "TBA";

builder.Services.AddAuthentication("JWTBearer")
	.AddJwtBearer("JWTBearer", options =>
	{
		options.Authority = authAuthority;
		
		if (builder.Environment.IsDevelopment())
		{
			options.RequireHttpsMetadata = false;
		}
		
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = false,
			ValidAudiences = new List<string> { "tradingGlossary" },
			ValidIssuers = new List<string> { 
				"TBA", 
			}
		};
	});

var app = builder.Build();

// ### HTTP Request Pipeline konfigurieren ###
if (app.Environment.IsDevelopment())
{
	// LOKALE ENTWICKLUNG
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();

	app.UseCors(policy => policy
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
}
else
{
	app.UseHttpsRedirection();

	string allowedClientUrl = app.Configuration["AppConfig:AllowedClientUrl"] ?? "";
	
	if (!string.IsNullOrEmpty(allowedClientUrl))
	{
		app.UseCors(policy => policy
			.WithOrigins(allowedClientUrl)
			.AllowAnyHeader()
			.AllowAnyMethod());
		//https://chartops.de/api/user/login
	}
	else 
	{
		app.Logger.LogWarning("CORS: AllowedClientUrl is not set!");
	}

	app.UseAuthentication();
	app.UseAuthorization();
}

// Datenbank-Migration beim Start
// using (var scope = app.Services.CreateScope())
// {
// 	var dbContext = scope.ServiceProvider.GetRequiredService<TradingGlossaryDbContext>();
// 	try
// 	{
// 		dbContext.Database.Migrate();
// 	}
// 	catch (Exception ex)
// 	{
// 		var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
// 		logger.LogError(ex, "An error occurred while migrating the database.");
// 	}
// }

// Gemeinsame Middleware und Endpunkte
app.UseMiddleware<SessionInfoMiddleware>();
app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run();