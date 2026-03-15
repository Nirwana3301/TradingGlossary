using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingGlossary.Database.Model;

namespace TradingGlossary.Database.Configurations;

public class GlossaryEntryConfig : IEntityTypeConfiguration<GlossaryEntry>
{
    public void Configure(EntityTypeBuilder<GlossaryEntry> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.ModifiedBy)
            .HasMaxLength(256);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasColumnType("text");

        builder.HasOne(x => x.GlossaryLetter)
            .WithMany(x => x.GlossaryEntries)
            .HasForeignKey(x => x.GlossaryLetterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Title);

        builder.HasIndex(x => new { x.GlossaryLetterId, x.Title });
    }
}