using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TradingGlossary.Database.Model;

namespace TradingGlossary.Database.Database;

public class TradingGlossaryDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public TradingGlossaryDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public TradingGlossaryDbContext(DbContextOptions<TradingGlossaryDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<GlossaryLetter> GlossaryLetters { get; set; } 
    public DbSet<GlossaryEntry> GlossaryEntries { get; set;  }
    public DbSet<GlossaryTag> GlossaryTags { get; set; }
    public DbSet<GlossaryEntryTag> GlossaryEntryTags { get; set; }
	
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
	
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}