using Microsoft.EntityFrameworkCore;
using PdfGenerator.Core.Entities;

namespace PdfGenerator.Test.HtmlTemplateToPdf.Utilities;

public class DatabaseContext
{
    public static async Task<DataContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new DataContext(options);
        await databaseContext.Database.EnsureCreatedAsync();
        if (await databaseContext.HtmlTemplates.CountAsync() > 0) return databaseContext;
        for (var i = 1; i <= 10; i++)
        {
            databaseContext.HtmlTemplates.Add(new HtmlTemplate()
            {
                Id = Guid.NewGuid(),
                Name = $"testTemplate{i}",
                Content = "<p>Testing template by [user_name]",
                AdditionalContext = ""
            });
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }  
}