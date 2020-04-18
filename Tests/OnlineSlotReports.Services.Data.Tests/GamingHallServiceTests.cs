namespace OnlineSlotReports.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        [Fact]
        public async Task DeleteAsyncWithValidId()
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
            await service.DeleteAsync(hall.Id);

            var result = await dbContext.GamingHalls.Where(x => x.Id == hall.Id).FirstOrDefaultAsync();

            Assert.True(result == null);
        }

        [Fact]
        public async Task DeleteAsyncWithNullId()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            await service.AddAsync(
                  "hall",
                  "image",
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "1");

            await service.DeleteAsync(null);

            var result = await dbContext.GamingHalls.FirstOrDefaultAsync();

            Assert.Equal("hall", result.HallName);
            Assert.Equal("image", result.ImageUrl);
            Assert.Equal("desc", result.Description);
            Assert.Equal("1111", result.PhoneNumber);
            Assert.Equal("adress", result.Adress);
            Assert.Equal("town", result.Town);
            Assert.Equal("1", result.UserId);
        }

        [Fact]
        public async Task All()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            for (int i = 0; i < 10; i++)
            {
                await service.AddAsync(
                  "hall" + i,
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId" + i);
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            int page = 2;
            int itemPerPage = 3;

            var result = service.All<GamingHallsIndexViewModel>(itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var hall in result)
            {
                Assert.Equal("hall" + (count + ((page - 1) * itemPerPage)), hall.HallName);
                count++;
            }

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task AllOnTheLastPage()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            for (int i = 0; i < 10; i++)
            {
                await service.AddAsync(
                  "hall" + i,
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId" + i);
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            int page = 4;
            int itemPerPage = 3;

            var result = service.All<GamingHallsIndexViewModel>(itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var hall in result)
            {
                Assert.Equal("hall" + (count + ((page - 1) * itemPerPage)), hall.HallName);
                count++;
            }

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task AllHallsWithVaildID()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                  "hall" + i,
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId");
            }

            for (int i = 10; i <= 15; i++)
            {
                await service.AddAsync(
                  "hall" + i,
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId" + i);
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.AllHalls<GamingHallViewModel>("userId");
            int count = 0;
            foreach (var hall in result)
            {
                count++;
                Assert.Equal("hall" + count, hall.HallName);
            }

            Assert.Equal(5, count);
        }

        [Fact]
        public async Task AllHallsWithInvaildID()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                  "hall" + i,
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.AllHalls<GamingHallViewModel>("userIdInvalid");
            int count = 0;
            foreach (var hall in result)
            {
                count++;
            }

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task AllHallsWithNullID()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                  "hall" + i,
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.AllHalls<GamingHallViewModel>(null);
            int count = 0;
            foreach (var hall in result)
            {
                count++;
            }

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task AllOfChainGetAllWithSameName()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                  "hall" + i,
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId");
            }

            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                  "hall",
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.AllOfChain<GamingHallViewComponentModel>("hall");
            int count = 0;
            foreach (var hall in result)
            {
                count++;
                Assert.Equal("hall", hall.HallName);
            }

            Assert.Equal(5, count);
        }

        [Fact]
        public async Task AllOfChainGetAllWithNUllName()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                  "hall" + i,
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId");
            }

            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                  "hall",
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.AllOfChain<GamingHallViewComponentModel>(null);
            int count = 0;
            foreach (var hall in result)
            {
                count++;
            }

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task AllOfChainGetAllWithNoExistingName()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            var service = new GamingHallService(repository);
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                  "hall" + i,
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId");
            }

            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                  "hall",
                  null,
                  "desc",
                  "1111",
                  "adress",
                  "town",
                  "userId");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.AllOfChain<GamingHallViewComponentModel>("test");
            int count = 0;
            foreach (var hall in result)
            {
                count++;
            }

            Assert.Equal(0, count);
        }
    }
}
