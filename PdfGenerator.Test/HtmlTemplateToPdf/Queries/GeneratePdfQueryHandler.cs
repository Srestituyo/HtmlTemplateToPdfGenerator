using Microsoft.Extensions.Configuration;
using PdfGenerator.Application.Models;
using PdfGenerator.Application.Queries;

namespace PdfGenerator.Test.HtmlTemplateToPdf.Queries;

[TestClass]
public class GeneratePdfQueryHandler
{
    [Test]
    public async Task Handle_WhenTemplateNameNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
         var generatePdfModel = new GeneratePdfModel
        {
            TemplateName = "testTemplate",
            Context = new Dictionary<string, string>
            {
                { "user_name", "John Doe" }
            }
        };
        
        var aQuery = new GeneratePdfQuery(){GeneratePdfModel = generatePdfModel};
        var dataContextMock = await DatabaseContext.GetDatabaseContext();
        var configuration = new Mock<IConfiguration>();
        var handler = new GeneratePdfHandler(dataContextMock, configuration.Object);
        
        // Act and Assert
        Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await handler.Handle(aQuery, CancellationToken.None);
        });
        
    }
    
    [Test]
    public async Task Handle_WhenValidQuery_ReturnsBase64PdfData()
    {
        // Arrange
        var generatePdfModel = new GeneratePdfModel
        {
            TemplateName = "testTemplate1",
            Context = new Dictionary<string, string>
            {
                { "user_name", "John Doe" }
            }
        };
        
        var aQuery = new GeneratePdfQuery(){GeneratePdfModel = generatePdfModel};
        var dataContextMock = await DatabaseContext.GetDatabaseContext();
        var configuration = new Mock<IConfiguration>();

        var handler = new GeneratePdfHandler(dataContextMock, configuration.Object);
        
        // Act
        var result = await handler.Handle(aQuery, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Succeeded, Is.EqualTo(true));
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
        });
    }
}