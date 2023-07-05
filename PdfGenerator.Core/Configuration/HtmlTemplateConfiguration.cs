using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PdfGenerator.Core.Entities;

namespace PdfGenerator.Core.Configuration;

public class HtmlTemplateConfiguration : IEntityTypeConfiguration<HtmlTemplate>    
{
    public void Configure(EntityTypeBuilder<HtmlTemplate> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name);
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.AditionalContext).IsRequired();
    }
}