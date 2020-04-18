namespace OnlineSlotReports.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Data.Repositories;
    using OnlineSlotReports.Services.Data.MachineCountersServices;
    using OnlineSlotReports.Services.Data.Tests.Factory;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using Xunit;

    public class MachineCountersServiceTests
    {
        [Fact]
        public async Task AddAsyncWithCorectData()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var service = new MachineCountersService(new EfRepository<MachineCounters>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                1,
                1,
                2,
                2,
                date,
                "1");

            var result = await dbContext.MachineCounters.FirstOrDefaultAsync();

            Assert.Equal(1, result.ElIn);
            Assert.Equal(1, result.ElOut);
            Assert.Equal(2, result.MechIn);
            Assert.Equal(2, result.MechOut);
            Assert.Equal(date, result.Date);
            Assert.Equal("1", result.SlotMachineId);
        }
    }
}
