using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingGlossary.Database.Model;

namespace TradingGlossary.Database.Configurations;

public class GlossaryTagConfig : IEntityTypeConfiguration<GlossaryTag>
{
    public void Configure(EntityTypeBuilder<GlossaryTag> builder)
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

        builder.Property(x => x.Label)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.HasIndex(x => x.Label)
            .IsUnique();

        builder.HasIndex(x => x.SortOrder);
    }
}