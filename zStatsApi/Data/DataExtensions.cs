using Microsoft.EntityFrameworkCore;

namespace zStatsApi.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ZStatsContext>();
        dbContext.Database.Migrate();
    }
}