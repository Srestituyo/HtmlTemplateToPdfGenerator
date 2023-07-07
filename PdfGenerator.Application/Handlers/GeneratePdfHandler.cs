using System.Security.AccessControl;
using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.DTOs;
using PdfGenerator.Application.Queries;
using PdfGenerator.Infrastructure;
using PuppeteerSharp;
using Serilog;

namespace PdfGenerator.Application.Handlers;

public class GeneratePdfHandler : IRequestHandler<GeneratePdfQuery, Response<string>>
{
    private readonly DataContext _dataContext;
    private readonly IConfiguration _configuration;
    private string pupperEnvPath;

    public GeneratePdfHandler(DataContext theContext, IConfiguration configuration)
    {
        _dataContext = theContext;
        _configuration = configuration;
        this.pupperEnvPath = _configuration[$"PUPPETEER_EXECUTABLE_PATH:PATH"];

    }

    public async Task<Response<string>> Handle(GeneratePdfQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var aHtmlTemplate = await _dataContext.HtmlTemplates
                .AsNoTracking()
                .Where(x => x.Name == request.GeneratePdfModel.TemplateName)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);
        
            if (aHtmlTemplate == null)
            {
                throw new KeyNotFoundException($"The given HTML template name {request.GeneratePdfModel.TemplateName} was not found.");
            }
        
            // Replace the placeholders in the HTML template with the values from the request
            foreach (var field in request.GeneratePdfModel.Context)
            {
                aHtmlTemplate.Content =  aHtmlTemplate.Content.Replace("[" + field.Key + "]", field.Value);
            } 
            
            
            int index = aHtmlTemplate.Content.LastIndexOf("</html>", StringComparison.Ordinal);
            if (index != -1)
            {
                aHtmlTemplate.Content = aHtmlTemplate.Content.Insert(index,  aHtmlTemplate.AdditionalContext);
            }
        
            aHtmlTemplate.Content = Regex.Replace(aHtmlTemplate.Content, @"\[.*?\]", " ");

            aHtmlTemplate.Content = aHtmlTemplate.Content + " " + aHtmlTemplate.AdditionalContext;
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true,     ExecutablePath = this.pupperEnvPath }))
            using (var page = await browser.NewPageAsync())
            {
                await page.SetContentAsync(aHtmlTemplate.Content);
                var pdfData = await page.PdfDataAsync();
                return new Response<string>(Convert.ToBase64String(pdfData),null);
            }        
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw;
        }
    }
}