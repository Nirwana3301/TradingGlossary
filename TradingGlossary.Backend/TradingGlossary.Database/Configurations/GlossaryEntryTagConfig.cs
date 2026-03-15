using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingGlossary.Database.Model;

namespace TradingGlossary.Database.Configurations;

public class GlossaryEntryTagConfig : IEntityTypeConfiguration<GlossaryEntryTag>
{
    public void Configure(EntityTypeBuilder<GlossaryEntryTag> builder)
    {
        builder.HasKey(x => new { x.GlossaryEntryId, x.GlossaryTagId });

        builder.HasOne(x => x.GlossaryEntry)
            .WithMany(x => x.GlossaryEntryTags)
            .HasForeignKey(x => x.GlossaryEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.GlossaryTag)
            .WithMany(x => x.GlossaryEntryTags)
            .HasForeignKey(x => x.GlossaryTagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.GlossaryTagId);
    }
}