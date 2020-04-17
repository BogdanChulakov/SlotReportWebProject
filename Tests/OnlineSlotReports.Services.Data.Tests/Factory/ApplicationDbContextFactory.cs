namespace OnlineSlotReports.Services.Data.Tests.Factory
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Data;

    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext CreateDbContext()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            return dbContext;
        }
    }
}
