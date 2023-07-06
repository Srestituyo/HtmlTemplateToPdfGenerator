using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PdfGenerator.Core.Entities;

namespace PdfGenerator.Core.Configuration;

public class HtmlTemplateConfiguration : IEntityTypeConfiguration<HtmlTemplate>    
{
    public void Configure(EntityTypeBuilder<HtmlTemplate> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.AdditionalContext).IsRequired();
    }
}