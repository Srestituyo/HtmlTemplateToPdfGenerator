using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PdfGenerator.Core.Entities;

public class HtmlTemplate : BaseEntity
{
    public string Name { get; set; }

    public string Content { get; set; }
    
    public string AdditionalContext { get; set; }
}