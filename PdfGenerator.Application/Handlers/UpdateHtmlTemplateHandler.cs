using MediatR;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.Commands;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Infrastructure;
using Serilog;

namespace PdfGenerator.Application.Handlers;

public class UpdateHtmlTemplateHandler : IRequestHandler<UpdateHtmlTemplateCommand, Response<Guid>>
{
    private readonly DataContext _dataContext;

    public UpdateHtmlTemplateHandler(DataContext theDataContext)
    {
        _dataContext = theDataContext;
    }

    public async Task<Response<Guid>> Handle(UpdateHtmlTemplateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aHtmlTemplate = await _dataContext.HtmlTemplates
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (aHtmlTemplate == null)
            {
                throw new KeyNotFoundException($"The given HTML template Id {request.Id} was not found.");
            }
            
            var aExistingNameTemplate = await _dataContext.HtmlTemplates
                .Where(x => x.Name == request.HtmlTemplate.Name)
                .FirstOrDefaultAsync(cancellationToken);

            if (aExistingNameTemplate != null)
            {
                throw new InvalidOperationException("An HTML template with the given name already exists.");
            }

            aHtmlTemplate.Name = request.HtmlTemplate.Name;
            aHtmlTemplate.Content = request.HtmlTemplate.Content;
            aHtmlTemplate.AdditionalContext = request.HtmlTemplate.AdditionalContext;
            aHtmlTemplate.LastUpdatedDate = DateTime.Now;

            _dataContext.HtmlTemplates.Update(aHtmlTemplate);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return new Response<Guid>(aHtmlTemplate.Id);
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw;
        }
    }
}