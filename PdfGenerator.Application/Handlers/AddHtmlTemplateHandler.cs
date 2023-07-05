using MediatR;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.Commands;
using PdfGenerator.Application.Common.Exceptions;
using PdfGenerator.Application.Common.Helper;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Core.Entities;
using PdfGenerator.Infrastructure;

namespace PdfGenerator.Application.Handlers;

public class AddHtmlTemplateHandler : IRequestHandler<AddHtmlTemplateCommand, Response<Guid>>
{
    private readonly DataContext _dataContext;
   
    public AddHtmlTemplateHandler(DataContext theContext)
    {
        _dataContext = theContext;
    }
    public async Task<Response<Guid>> Handle(AddHtmlTemplateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aHtmlTemplate =
                await _dataContext.HtmlTemplates.Where(x => x.Name == request.Name).FirstOrDefaultAsync(cancellationToken);

            if (aHtmlTemplate != null)
            {
                throw new InvalidOperationException("An HTML template with the given name already exists.");
            }

            var aNewHtmlTemplate = new HtmlTemplate()
            {
                Name = request.Name,
                Content = request.Content,
                AditionalContext = request.AdditionalContent
            }; 

            await _dataContext.HtmlTemplates.AddAsync(aNewHtmlTemplate, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>(aNewHtmlTemplate.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}