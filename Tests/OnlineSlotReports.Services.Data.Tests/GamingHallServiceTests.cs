namespace OnlineSlotReports.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Common;
    using OnlineSlotReports.Data;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Data.Repositories;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Services.Data.Tests.Factory;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;
    using Xunit;

    public class GamingHallServiceTests
    {
        [Fact]
        public async Task AddAsyncWithImageUrl()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
        public async Task GetHallCountWithEntity()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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

        [Fact]
        public void GetHallCountWithotEntity()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);

            Assert.Equal(0, service.GetHallsCount());
        }

        [Fact]
        public async Task UpdateAsyncWithValidData()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
            var hall = await dbContext.GamingHalls.FirstOrDefaultAsync();
            await service.UpdateAsync(
                 hall.Id,
                 "hall",
                 "gttp://www.test.com",
                 "desc",
                 "11114",
                 "adress",
                 "town1");
            var result = await dbContext.GamingHalls.FirstOrDefaultAsync();

            Assert.Equal("hall", result.HallName);
            Assert.Equal("gttp://www.test.com", result.ImageUrl);
            Assert.Equal("desc", result.Description);
            Assert.Equal("11114", result.PhoneNumber);
            Assert.Equal("adress", result.Adress);
            Assert.Equal("town1", result.Town);
        }

        [Fact]
        public async Task UpdateAsyncWithInvalidData()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
            var hall = await dbContext.GamingHalls.FirstOrDefaultAsync();
            await service.UpdateAsync(
                 hall.Id,
                 null,
                 "www.test.com",
                 "desc",
                 "11114d",
                 "adress",
                 "town1");
            var result = await dbContext.GamingHalls.FirstOrDefaultAsync();

            Assert.Equal(hall.HallName, result.HallName);
            Assert.Equal(hall.ImageUrl, result.ImageUrl);
            Assert.Equal(hall.Description, result.Description);
            Assert.Equal(hall.PhoneNumber, result.PhoneNumber);
            Assert.Equal(hall.Adress, result.Adress);
            Assert.Equal(hall.Town, result.Town);
        }

        [Fact]
        public async Task GetTWithValidId()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
            var hall = await dbContext.GamingHalls.FirstOrDefaultAsync();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.GetT<DetailsViewModel>(hall.Id);

            Assert.Equal(hall.HallName, result.HallName);
            Assert.Equal(hall.ImageUrl, result.ImageUrl);
            Assert.Equal(hall.Description, result.Description);
            Assert.Equal(hall.PhoneNumber, result.PhoneNumber);
            Assert.Equal(hall.Adress, result.Adress);
            Assert.Equal(hall.Town, result.Town);
        }

        [Fact]
        public async Task GetTWithInvalidId()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.GetT<DetailsViewModel>("1234567");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetTWithNullId()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.GetT<DetailsViewModel>(null);

            Assert.Null(result);
        }

        [Fact]
        public async Task SearchByName()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
            await service.AddAsync(
                  "hall2",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var halls = service.Search<IndexGamingHallViewModel>("hall1");

            foreach (var hall in halls)
            {
                Assert.Equal("hall1", hall.HallName);
            }
        }

        [Fact]
        public async Task SearchByTown()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
            await service.AddAsync(
                  "hall2",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var halls = service.Search<IndexGamingHallViewModel>("town");

            foreach (var hall in halls)
            {
                Assert.Equal("town", hall.Town);
            }
        }

        [Fact]
        public async Task SearchByNevalideString()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
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
            await service.AddAsync(
                  "hall2",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var halls = service.Search<IndexGamingHallViewModel>("non-existent");
            int count = 0;
            foreach (var hall in halls)
            {
                count++;
            }

            Assert.Equal(0, count);
        }
    }
}
