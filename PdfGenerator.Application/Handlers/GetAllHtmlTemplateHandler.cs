using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Application.Common.Wrapper;
using PdfGenerator.Application.DTOs;
using PdfGenerator.Application.Queries;
using PdfGenerator.Application.ViewModels;
using PdfGenerator.Infrastructure;
using Serilog;

namespace PdfGenerator.Application.Handlers;

public class GetAllHtmlTemplateHandler : IRequestHandler<GetAllHtmlTemplateQuery, Response<HtmlTemplateViewModel>>
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public GetAllHtmlTemplateHandler(DataContext theContext, IMapper theMapper)
    {
        _dataContext = theContext;
        _mapper = theMapper;
    }

    public async Task<Response<HtmlTemplateViewModel>> Handle(GetAllHtmlTemplateQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var aHtmlTemplateVm = new HtmlTemplateViewModel()
            {
                HtmlTemplateList = await _dataContext.HtmlTemplates.ProjectTo<HtmlTemplateDTO>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken)
            };

            return new Response<HtmlTemplateViewModel>(aHtmlTemplateVm);
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            throw;
        }
    }
}