using MediatR;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.Commands;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Infrastructure;

namespace PdfGenerator.Application.Handlers;

public class RemoveHtmlTemplateByIdHandler : IRequestHandler<RemoveHtmlTemplateByIdCommand, Response<Guid>>
{
    private readonly DataContext _dataContext;

    public RemoveHtmlTemplateByIdHandler(DataContext theDataContext)
    {
        _dataContext = theDataContext;
    }
    
    public async Task<Response<Guid>> Handle(RemoveHtmlTemplateByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aHtmlTemplate = await _dataContext.HtmlTemplates.Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (aHtmlTemplate == null)
            {
                throw new KeyNotFoundException($"The given HTML template Id {request.Id} was not found.");
            }

            _dataContext.HtmlTemplates.Remove(aHtmlTemplate);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return new Response<Guid>(request.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}