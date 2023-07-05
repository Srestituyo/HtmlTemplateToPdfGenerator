using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Application.Models;
using PdfGenerator.Application.Queries;

namespace PdfGenerator.WebApi.Controllers;

public class GeneratePdfController : ApiControllerBase
{
    
    [HttpPost]
    public async Task<ActionResult> GeneratePdf([FromBody] GeneratePdfModel generatePdfModel)
    {
        var aResponse = await Mediator.Send(new GeneratePdfQuery()
        {
            GeneratePdfModel = generatePdfModel
        });
        
        
        return Ok(aResponse);
    }
}