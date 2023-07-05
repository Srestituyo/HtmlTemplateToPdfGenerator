using AutoMapper;
using PdfGenerator.Application.Common.Mapping;
using PdfGenerator.Core.Entities;

namespace PdfGenerator.Application.DTOs;

public class HtmlTemplateDTO  : IMapFrom<HtmlTemplate>
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Content { get; set; }

    public string AdditionalContent { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<HtmlTemplate, HtmlTemplateDTO>();
    }
}