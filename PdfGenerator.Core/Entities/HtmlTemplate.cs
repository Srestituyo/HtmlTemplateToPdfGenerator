namespace PdfGenerator.Core.Entities;

public class HtmlTemplate : BaseEntity
{
    public string Name { get; set; }

    public string Content { get; set; }

    public string AditionalContext { get; set; }
}