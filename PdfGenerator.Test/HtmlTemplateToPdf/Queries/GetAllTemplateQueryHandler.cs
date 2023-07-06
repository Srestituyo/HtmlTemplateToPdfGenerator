using PdfGenerator.Application.DTOs;
using PdfGenerator.Application.Queries;
using PdfGenerator.Core.Entities;

namespace PdfGenerator.Test.HtmlTemplateToPdf.Queries;

[TestClass]
public class GetAllTemplateQueryHandler
{
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    
    [Test]
    public async Task Handler_WhenValidQuery_ReturnTemplateList()
    {
        // Arrange
        var aExpectedResult = true;
        var aQuery = new GetAllHtmlTemplateQuery();
        var dataContextMock = await DatabaseContext.GetDatabaseContext();  
        _mapper.Setup(m => m.ConfigurationProvider).Returns(() => new MapperConfiguration(cfg =>
        {
            /* configure mappings here */ 
            cfg.CreateMap<HtmlTemplate, HtmlTemplateDTO>();

        }));

        var handler = new GetAllHtmlTemplateHandler(dataContextMock, _mapper.Object);
        
        // Act 
        var result = handler.Handle(aQuery, default);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Result.Succeeded, Is.EqualTo(aExpectedResult));
            Assert.That(result.Result.Data.HtmlTemplateList.Count, Is.EqualTo(10));
        });
        
    }
}