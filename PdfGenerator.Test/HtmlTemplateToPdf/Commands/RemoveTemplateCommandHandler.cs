using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.DTOs;
using PdfGenerator.Application.Queries;
using PdfGenerator.Core.Entities;

namespace PdfGenerator.Test.HtmlTemplateToPdf.Commands;

[TestClass]
public class RemoveTemplateCommandHandler
{
    [Test]
    public async Task Handler_WhenCommandIsValid_ReturnGuidOfTemplateRemoved()
    {
        // Arrange
        var aExpectedResult = true;
        var dataContextMock = await DatabaseContext.GetDatabaseContext();
        var templateId = dataContextMock.HtmlTemplates.FirstOrDefaultAsync().Result.Id;
        
        var aCommand = new RemoveHtmlTemplateByIdCommand(){Id = templateId};
        
        var handler = new RemoveHtmlTemplateByIdHandler(dataContextMock);
        // Act 
        var result = handler.Handle(aCommand, default);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Result.Succeeded, Is.EqualTo(aExpectedResult));
            Assert.That(result.Result.Data, Is.EqualTo(templateId));
        });
        
    }
    
    [Test]
    public async Task Handler_Should_WhenTemplateIdNotFound_ReturnKeyNotFoundException()
    {
        // Arrange
        var aTemplateId = Guid.NewGuid();
         var dataContextMock = await DatabaseContext.GetDatabaseContext();
         
        var aCommand = new RemoveHtmlTemplateByIdCommand(){Id = aTemplateId};

        var handler = new RemoveHtmlTemplateByIdHandler(dataContextMock);
        // Act and Assert
        Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await handler.Handle(aCommand, CancellationToken.None);
        });
        
    }
}