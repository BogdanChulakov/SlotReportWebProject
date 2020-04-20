namespace OnlineSlotReports.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Data;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Data.Repositories;
    using OnlineSlotReports.Services.Data.EmployeesServices;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Services.Data.Tests.Factory;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using OnlineSlotReports.Web.ViewModels.SlotMachinesViewModels;
    using Xunit;

    [Collection("Mappings collection")]
    public class SlotMachinesServiceTests
    {
        [Fact]
        public async Task AddAsyncWithCorectData()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new SlotMachinesService(new EfDeletableEntityRepository<SlotMachine>(dbContext));
            await service.AddAsync(
                "1234567890",
                "Megajack",
                1,
                "1");

            var result = await dbContext.SlotMachines.FirstOrDefaultAsync();

            Assert.Equal("1234567890", result.LicenseNumber);
            Assert.Equal("Megajack", result.Model);
            Assert.Equal(1, result.NumberInHall);
            Assert.Equal("1", result.GamingHallId);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new SlotMachinesService(new EfDeletableEntityRepository<SlotMachine>(dbContext));
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                 "" + i,
                 "Megajack",
                 1,
                 "1");
            }

            var results = service.All<SlotMachineViewModel>("1");
            int count = 0;
            foreach (var result in results)
            {
                count++;
                Assert.Equal("" + count, result.LicenseNumber);
                Assert.Equal("Megajack", result.Model);
                Assert.Equal(1, result.NumberInHall);
            }

            Assert.Equal(5, count);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllWithNullId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new SlotMachinesService(new EfDeletableEntityRepository<SlotMachine>(dbContext));
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                 "" + i,
                 "Megajack",
                 1,
                 "1");
            }

            var results = service.All<SlotMachineViewModel>(null);
            int count = 0;
            foreach (var result in results)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllWithNoExistingId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new SlotMachinesService(new EfDeletableEntityRepository<SlotMachine>(dbContext));
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                 "" + i,
                 "Megajack",
                 1,
                 "1");
            }

            var results = service.All<SlotMachineViewModel>("2");
            int count = 0;
            foreach (var result in results)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task DeleteAsyncWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new SlotMachinesService(new EfDeletableEntityRepository<SlotMachine>(dbContext));
            await service.AddAsync(
                "1234567890",
                "Megajack",
                1,
                "1");

            var slotMachine = await dbContext.SlotMachines.FirstOrDefaultAsync();

            await service.DeleteAsync(slotMachine.Id);

            var result = await dbContext.GamingHalls.Where(x => x.Id == slotMachine.Id).FirstOrDefaultAsync();

            Assert.True(result == null);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task GetHallIdWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new SlotMachinesService(new EfDeletableEntityRepository<SlotMachine>(dbContext));
            await service.AddAsync(
                "1234567890",
                "Megajack",
                1,
                "1");

            var slotMachine = await dbContext.SlotMachines.FirstOrDefaultAsync();

            var result = service.GetHallId(slotMachine.Id);

            Assert.Equal("1", result);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task GetByIdWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new SlotMachinesService(new EfDeletableEntityRepository<SlotMachine>(dbContext));
            await service.AddAsync(
                "1234567890",
                "Megajack",
                1,
                "1");

            var slotMachine = await dbContext.SlotMachines.FirstOrDefaultAsync();

            var result = service.GetById<IndexViewModel>(slotMachine.Id);

            Assert.True(result != null);
            Assert.Equal(slotMachine.Id, result.Id);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task GetByIdWithInvalidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new SlotMachinesService(new EfDeletableEntityRepository<SlotMachine>(dbContext));
            await service.AddAsync(
                "1234567890",
                "Megajack",
                1,
                "1");

            var result = service.GetById<IndexViewModel>("invalidId");

            Assert.True(result == null);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task GetByIdWithNullId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new SlotMachinesService(new EfDeletableEntityRepository<SlotMachine>(dbContext));
            await service.AddAsync(
                "1234567890",
                "Megajack",
                1,
                "1");

            var result = service.GetById<IndexViewModel>(null);

            Assert.True(result == null);
            dbContext.Database.EnsureDeleted();
        }
    }
}
