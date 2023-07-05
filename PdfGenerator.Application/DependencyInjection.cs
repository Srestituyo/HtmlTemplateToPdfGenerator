using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PdfGenerator.Application.Common.Behaviours;

namespace PdfGenerator.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(msc => {
            msc.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            msc.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }); 

        return services;
    }
}