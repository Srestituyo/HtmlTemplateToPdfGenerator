using MediatR;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.Commands;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Infrastructure;

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
            var aHtmlTemplate = await _dataContext.HtmlTemplates.Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (aHtmlTemplate == null)
            {
                throw new KeyNotFoundException($"The given HTML template Id {request.Id} was not found.");
            }

            aHtmlTemplate.Name = request.HtmlTemplate.Name;
            aHtmlTemplate.Content = request.HtmlTemplate.Content;
            aHtmlTemplate.AditionalContext = request.HtmlTemplate.AdditionalContent;
            aHtmlTemplate.LastUpdatedDate = DateTime.Now;

            _dataContext.HtmlTemplates.Update(aHtmlTemplate);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return new Response<Guid>(aHtmlTemplate.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}