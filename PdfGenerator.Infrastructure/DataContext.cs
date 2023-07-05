using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PdfGenerator.Core.Entities;

namespace PdfGenerator.Infrastructure;

public class DataContext : DbContext
{
    public DbSet<HtmlTemplate> HtmlTemplates {get; set; }
    
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    } 

    // protected override void OnModelCreating(ModelBuilder theModelBuilder)
    // { 
    //     theModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    // }

}