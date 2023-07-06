using System.Net;
using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Application.Commands;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.Models;
using PdfGenerator.Application.Queries;
using PdfGenerator.Application.ViewModels;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace PdfGenerator.WebApi.Controllers;

public class TemplateController : ApiControllerBase
{
 
    public TemplateController()
    {
    }
    
    /// <summary>
    /// Retrieves all HTML templates.
    /// </summary>
    /// <returns>HtmlTemplateViewModel</returns>
    [SwaggerResponse((int)HttpStatusCode.OK, "Retrieves all HTML templates.", typeof(Response<HtmlTemplateViewModel>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Response<string>))]
    [HttpGet]
    public async Task<ActionResult> GetAllTemplate()
    {
        var aResponse = await Mediator.Send(new GetAllHtmlTemplateQuery());
        
        if (!aResponse.Succeeded)
            Log.Error(aResponse.Message);
        else
            Log.Information(aResponse.Message);
        
        return Ok(aResponse);
    }
    
    /// <summary>
    /// Retrieves a specific HTML template by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>HtmlTemplateModel</returns>
    [SwaggerResponse((int)HttpStatusCode.OK, "Retrieves a specific HTML template by ID.", typeof(Response<HtmlTemplateModel>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Response<string>))]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetTemplateById([FromRoute] Guid id)
    {
        var aResponse = await Mediator.Send(new GetHtmlTemplateByIdQuery()
        {
            Id = id
        });
        
        if (!aResponse.Succeeded)
            Log.Error(aResponse.Message);
        else
            Log.Information(aResponse.Message);
        
        return Ok(aResponse);
    }
    
    /// <summary>
    /// Retrieves a specific HTML template by name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>HtmlTemplateModel</returns>
    [SwaggerResponse((int)HttpStatusCode.OK, "Retrieves a specific HTML template by name.", typeof(Response<HtmlTemplateModel>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Response<string>))]
    [HttpGet("{name}")]
    public async Task<ActionResult> GetTemplateByName([FromRoute] string name)
    {
        var aResponse = await Mediator.Send(new GetHtmlTemplateByNameQuery()
        {
            Name = name
        });
        
        if (!aResponse.Succeeded)
            Log.Error(aResponse.Message);
        else
            Log.Information(aResponse.Message);
        
        return Ok(aResponse);
    }
    
    /// <summary>
    /// Add a new HTML template.
    /// </summary>
    /// <param name="theHtmlTemplate"></param>
    /// <returns>AddHtmlTemplateCommand</returns>
    [SwaggerResponse((int)HttpStatusCode.OK, "Add a new HTML template.", typeof(Response<AddHtmlTemplateCommand>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Response<string>))]
    [HttpPost]
    public async Task<ActionResult> AddTemplate([FromBody] AddHtmlTemplateCommand theHtmlTemplate)
    {
        var aResponse = await Mediator.Send(theHtmlTemplate);
        
        if (!aResponse.Succeeded)
            Log.Error(aResponse.Message);
        else
            Log.Information(aResponse.Message);
        
        return Ok(aResponse);
    }
    
    /// <summary>
    /// Generates a PDF based on the specified HTML template and provided context.
    /// </summary>
    /// <param name="generatePdfModel"></param>
    /// <returns>GeneratePdfModel</returns>
    [SwaggerResponse((int)HttpStatusCode.OK, "Generates a PDF based on the specified HTML template and provided context.", typeof(Response<GeneratePdfModel>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Response<string>))]
    [HttpPost("generate-pdf")]
    public async Task<ActionResult> GeneratePdf([FromBody] GeneratePdfModel generatePdfModel)
    {
        var aResponse = await Mediator.Send(new GeneratePdfQuery()
        {
            GeneratePdfModel = generatePdfModel
        }); 
        
        if (!aResponse.Succeeded)
            Log.Error(aResponse.Message);
        else
            Log.Information(aResponse.Message);
        
        return Ok(aResponse);
    }
    
    /// <summary>
    /// Updates an existing HTML template by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="theHtmlTemplate"></param>
    /// <returns>HtmlTemplateModel</returns>
    [SwaggerResponse((int)HttpStatusCode.OK, "Updates an existing HTML template by ID.", typeof(Response<HtmlTemplateModel>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Response<string>))]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateTemplate([FromRoute] Guid id, [FromBody] HtmlTemplateModel theHtmlTemplate)
    {
        var aResponse = await Mediator.Send(new UpdateHtmlTemplateCommand(id,theHtmlTemplate));

        if (!aResponse.Succeeded)
            Log.Error(aResponse.Message);
        else
            Log.Information(aResponse.Message);
        
        return Ok(aResponse);
    }
    
    /// <summary>
    /// Deletes an HTML template by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Guid</returns>
    [SwaggerResponse((int)HttpStatusCode.OK, "Deletes an HTML template by ID.", typeof(Response<Guid>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Response<string>))]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> RemoveTemplate([FromRoute] Guid id)
    {
        var aResponse = await Mediator.Send(new RemoveHtmlTemplateByIdCommand()
        {
            Id = id
        });
        
        if (!aResponse.Succeeded)
            Log.Error(aResponse.Message);
        else
            Log.Information(aResponse.Message);
        
        return Ok(aResponse);
    }
}