namespace OnlineSlotReports.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Data;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Data.Repositories;
    using OnlineSlotReports.Services.Data.MachineCountersServices;
    using OnlineSlotReports.Services.Data.ReportServices;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using OnlineSlotReports.Web.ViewModels.ReportsViewModels;
    using Xunit;

    [Collection("Mappings collection")]
    public class ReportServiceTests
    {
        [Fact]
        public async Task AddAsyncWithCorectData()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);

            var machineContersService = new MachineCountersService(new EfRepository<MachineCounters>(dbContext));

            var service = new ReportService(new EfDeletableEntityRepository<Report>(dbContext), machineContersService);
            var data = DateTime.UtcNow;
            await service.AddAsync(data, 1, 1, "1");

            var report = await dbContext.Reports.FirstOrDefaultAsync();

            Assert.Equal(data, report.Date);
            Assert.Equal(1, report.InForDay);
            Assert.Equal(1, report.OutForDay);
            Assert.Equal("1", report.GamingHallId);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllWithCorectId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);

            var machineContersService = new MachineCountersService(new EfRepository<MachineCounters>(dbContext));

            var service = new ReportService(new EfDeletableEntityRepository<Report>(dbContext), machineContersService);
            var data = DateTime.UtcNow;
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(data, 1 + i, 1 + i, "1");
            }

            var reports = service.All<IndexReportViewModel>("1");

            int count = 0;

            foreach (var report in reports)
            {
                count++;
                Assert.Equal(data, report.Date);
                Assert.Equal(1 + count, report.InForDay);
                Assert.Equal(1 + count, report.OutForDay);
                Assert.Equal("1", report.GamingHallId);
            }

            Assert.Equal(5, count);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllWithInvalidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);

            var machineContersService = new MachineCountersService(new EfRepository<MachineCounters>(dbContext));

            var service = new ReportService(new EfDeletableEntityRepository<Report>(dbContext), machineContersService);
            var data = DateTime.UtcNow;
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(data, 1 + i, 1 + i, "1");
            }

            var reports = service.All<IndexReportViewModel>("11");

            int count = 0;

            foreach (var report in reports)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllForAPeriodWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);

            var machineContersService = new MachineCountersService(new EfRepository<MachineCounters>(dbContext));

            var service = new ReportService(new EfDeletableEntityRepository<Report>(dbContext), machineContersService);

            var data1 = DateTime.UtcNow;
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(data1, 1, 1, "1");
            }

            var data2 = DateTime.UtcNow;

            var reports = service.AllForAPeriod<IndexReportViewModel>("1", data1, data2);

            int count = 0;

            foreach (var report in reports)
            {
                count++;
            }

            Assert.Equal(5, count);
            dbContext.Database.EnsureDeleted();
        }
    }
}
