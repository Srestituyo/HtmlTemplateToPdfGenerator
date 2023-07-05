namespace PdfGenerator.Core;

public class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
}