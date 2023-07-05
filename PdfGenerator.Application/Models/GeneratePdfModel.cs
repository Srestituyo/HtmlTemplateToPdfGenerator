namespace PdfGenerator.Application.Models;

public class GeneratePdfModel
{
    public string TemplateName { get; set; }
    
    public Dictionary<string, string> Context { get; set; }
}