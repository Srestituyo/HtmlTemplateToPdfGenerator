using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.DTOs;
using PdfGenerator.Application.Queries;
using PdfGenerator.Core.Entities;

namespace PdfGenerator.Test.HtmlTemplateToPdf.Queries;

[TestClass]
public class GetHtmlTemplateByIdQueryHandler
{
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    
    [Test]
    public async Task Handler_WhenValidQuery_ReturnTemplate()
    {
        // Arrange
        var aExpectedResult = true;
        var dataContextMock = await DatabaseContext.GetDatabaseContext();
        var templateId = dataContextMock.HtmlTemplates.FirstOrDefaultAsync().Result.Id;
        
        var aQuery = new GetHtmlTemplateByIdQuery(){Id = templateId};

        _mapper.Setup(m => m.ConfigurationProvider).Returns(() => new MapperConfiguration(cfg =>
        {
            /* configure mappings here */ 
            cfg.CreateMap<HtmlTemplate, HtmlTemplateDTO>();

        }));

        var handler = new GetHtmlTemplateByIdHandler(dataContextMock, _mapper.Object);
        // Act 
        var result = handler.Handle(aQuery, default);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Result.Data);
            Assert.IsNull(result.Result.Errors);
        });
        
    }
    
    [Test]
    public async Task Handler_WhenTemplateIdNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
         var aTemplateId = Guid.NewGuid();
         var dataContextMock = await DatabaseContext.GetDatabaseContext();
         var aQuery = new GetHtmlTemplateByIdQuery(){Id = aTemplateId};

        _mapper.Setup(m => m.ConfigurationProvider).Returns(() => new MapperConfiguration(cfg =>
        {
             cfg.CreateMap<HtmlTemplate, HtmlTemplateDTO>();

        }));

        var handler = new GetHtmlTemplateByIdHandler(dataContextMock, _mapper.Object);
        
        // Act and Assert
        Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await handler.Handle(aQuery, CancellationToken.None);
        });
        
    }

}