namespace OnlineSlotReports.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Common;
    using OnlineSlotReports.Data;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Data.Repositories;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using Xunit;

    public class GamingHallServiceTests
    {
        [Fact]
        public async Task AddAsyncWithImageUrl()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GamingHallTestDb1").Options);
            GamingHallService service = new GamingHallService(new EfDeletableEntityRepository<GamingHall>(dbContext));
            await service.AddAsync(
                "hall1",
                "image1",
                "desc1",
                "1111",
                "adress1",
                "town",
                "1");
            var result = await dbContext.GamingHalls.FirstOrDefaultAsync();
            Assert.Equal("hall1", result.HallName);
            Assert.Equal("image1", result.ImageUrl);
            Assert.Equal("desc1", result.Description);
            Assert.Equal("1111", result.PhoneNumber);
            Assert.Equal("adress1", result.Adress);
            Assert.Equal("town", result.Town);
            Assert.Equal("1", result.UserId);
        }

        [Fact]
        public async Task AddAsyncWithoutImageUrl()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GamingHallTestDb2").Options);
            var service = new GamingHallService(new EfDeletableEntityRepository<GamingHall>(dbContext));
            await service.AddAsync(
                "hall1",
                null,
                "desc1",
                "1111",
                "adress1",
                "town",
                "1");
            var result = await dbContext.GamingHalls.FirstOrDefaultAsync();
            Assert.Equal("hall1", result.HallName);
            Assert.Equal(GlobalConstants.DefaultLogo, result.ImageUrl);
            Assert.Equal("desc1", result.Description);
            Assert.Equal("1111", result.PhoneNumber);
            Assert.Equal("adress1", result.Adress);
            Assert.Equal("town", result.Town);
            Assert.Equal("1", result.UserId);
        }

        [Fact]
        public async Task GetHallCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: "GamingHallTestDb3").Options;
            var dbContext = new ApplicationDbContext(options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            await service.AddAsync(
                  "hall1",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");
            Assert.Equal(1, service.GetHallsCount());
        }
    }
}
