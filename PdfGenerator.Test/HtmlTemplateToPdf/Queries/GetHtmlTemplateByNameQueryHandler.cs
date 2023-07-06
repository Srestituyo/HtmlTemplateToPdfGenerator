using PdfGenerator.Application.DTOs;
using PdfGenerator.Application.Queries;
using PdfGenerator.Core.Entities;

namespace PdfGenerator.Test.HtmlTemplateToPdf.Queries;

[TestClass]
public class GetHtmlTemplateByNameQueryHandler
{
      private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    
    [Test]
    public async Task Handler_WhenValidTemplate_ReturnTemplate()
    {
        // Arrange
        var aExpectedResult = true;
        var dataContextMock = await DatabaseContext.GetDatabaseContext();
         
        var aQuery = new GetHtmlTemplateByNameQuery(){Name = "testTemplate1"};

        _mapper.Setup(m => m.ConfigurationProvider).Returns(() => new MapperConfiguration(cfg =>
        {
            /* configure mappings here */ 
            cfg.CreateMap<HtmlTemplate, HtmlTemplateDTO>();

        }));

        var handler = new GetHtmlTemplateByNameHandler(dataContextMock, _mapper.Object);
        
        // Act 
        var result = handler.Handle(aQuery, default);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Result.Succeeded, Is.EqualTo(aExpectedResult));
            Assert.That(result.Result.Data, Is.Not.Null);
        });
        
    }
    
    [Test]
    public async Task Handler_WhenTemplateNameNotFound_ReturnKeyNotFoundException()
    {
        // Arrange
         var aTemplateName = "template_new";
         var dataContextMock = await DatabaseContext.GetDatabaseContext();
         
        var aQuery = new GetHtmlTemplateByNameQuery(){Name = aTemplateName};

        _mapper.Setup(m => m.ConfigurationProvider).Returns(() => new MapperConfiguration(cfg =>
        {
            /* configure mappings here */ 
            cfg.CreateMap<HtmlTemplate, HtmlTemplateDTO>();

        }));

        var handler = new GetHtmlTemplateByNameHandler(dataContextMock, _mapper.Object);
                
        // Act and Assert
        Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await handler.Handle(aQuery, CancellationToken.None);
        });
        
    }
}