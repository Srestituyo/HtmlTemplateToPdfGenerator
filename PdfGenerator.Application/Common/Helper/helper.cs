using Ganss.Xss;

namespace PdfGenerator.Application.Common.Helper;

public static class helper
{
    public static bool ValidateHtml(string theHtmlContent)
    {
        // Create a new HtmlSanitizer instance
        var sanitizer = new HtmlSanitizer();

        // Sanitize and validate the HTML content
        string sanitizedHtml = sanitizer.Sanitize(theHtmlContent);

        // Check if any changes were made during sanitization
        if (theHtmlContent != sanitizedHtml)
        {
            // Invalid HTML or potential XSS attack detected
            Console.WriteLine("HTML content is invalid or contains potentially unsafe elements.");
            Console.WriteLine("Sanitized HTML: " + sanitizedHtml);
            return false;
        }
        else
        {
            // Valid HTML
            Console.WriteLine("HTML content is valid.");
            return true;
        }
    }
}