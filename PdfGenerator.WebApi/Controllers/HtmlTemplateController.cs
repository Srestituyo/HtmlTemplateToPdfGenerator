using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Application.Commands;
using PdfGenerator.Application.Handlers;
using PdfGenerator.Application.Models;
using PdfGenerator.Application.Queries;

namespace PdfGenerator.WebApi.Controllers;

public class HtmlTemplateController : ApiControllerBase
{
 
    public HtmlTemplateController()
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetAllHtmlTemplate()
    {
        var aResult = await Mediator.Send(new GetAllHtmlTemplateQuery());
        return Ok(aResult);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetHtmlTemplateById([FromRoute] Guid id)
    {
        var aResult = await Mediator.Send(new GetHtmlTemplateByIdQuery(){Id = id});
        return Ok(aResult);
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult> GetHtmlTemplateByName([FromRoute] string name)
    {
        var aResult = await Mediator.Send(new GetHtmlTemplateByNameQuery(){Name = name});
        return Ok(aResult);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddHtmlTemplate([FromBody] AddHtmlTemplateCommand theHtmlTemplate)
    {
        var aResult = await Mediator.Send(theHtmlTemplate);
        return Ok(aResult);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateHtmlTemplate([FromRoute] Guid id, [FromBody] HtmlTemplateModel theHtmlTemplate)
    {
        var aResult = await Mediator.Send(new UpdateHtmlTemplateCommand(){Id = id, HtmlTemplate = theHtmlTemplate});
        return Ok(aResult);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> RemoveHtmlTemplate([FromRoute] Guid id)
    {
        var aResult = await Mediator.Send(new RemoveHtmlTemplateByIdCommand(){Id = id});
        return Ok(aResult);
    }
}