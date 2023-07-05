using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration; 

namespace PdfGenerator.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var aConnectionString = configuration.GetConnectionString("DefaultConnection");
        // add db contexts
        services.AddDbContext<DataContext>(options =>
        {
            options.UseMySql(aConnectionString,ServerVersion.AutoDetect(aConnectionString) ,b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
        }); 
    }
}