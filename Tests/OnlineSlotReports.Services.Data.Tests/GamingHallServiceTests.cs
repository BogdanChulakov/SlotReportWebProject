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
    using OnlineSlotReports.Services.Data.UsersHallsServices;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;
    using Xunit;

    [Collection("Mappings collection")]
    public class GamingHallServiceTests
    {
        [Fact]
        public async Task AddAsyncWithImageUrl()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            GamingHallService service = new GamingHallService(new EfDeletableEntityRepository<GamingHall>(dbContext), usersHallsService);
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
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddAsyncWithoutImageUrl()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(new EfDeletableEntityRepository<GamingHall>(dbContext), usersHallsService);
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
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAllHallCountWithEntity()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
            await service.AddAsync(
                  "hall1",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");
            Assert.Equal(1, service.GetAllHallsCount());
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public void GetAllHallCountWithotEntity()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);

            Assert.Equal(0, service.GetAllHallsCount());
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetSearchCountWithEntity()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
            await service.AddAsync(
                  "hall1",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");
            Assert.Equal(1, service.GetSearchHallsCount("hall1"));
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetSearchCountWithNoexistingName()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
            await service.AddAsync(
                  "hall1",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");
            Assert.Equal(0, service.GetSearchHallsCount("hall11"));
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdateAsyncWithValidData()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task UpdateAsyncWithInvalidData()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task GetTWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
            await service.AddAsync(
                  "hall1",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");
            var hall = await dbContext.GamingHalls.FirstOrDefaultAsync();

            var result = service.GetT<DetailsViewModel>(hall.Id);

            Assert.Equal(hall.HallName, result.HallName);
            Assert.Equal(hall.ImageUrl, result.ImageUrl);
            Assert.Equal(hall.Description, result.Description);
            Assert.Equal(hall.PhoneNumber, result.PhoneNumber);
            Assert.Equal(hall.Adress, result.Adress);
            Assert.Equal(hall.Town, result.Town);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task GetTWithInvalidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
            await service.AddAsync(
                  "hall1",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");

            var result = service.GetT<DetailsViewModel>("1234567");

            Assert.Null(result);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task GetTWithNullId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
            await service.AddAsync(
                  "hall1",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");

            var result = service.GetT<DetailsViewModel>(null);

            Assert.Null(result);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task SearchByName()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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
            int page = 1;
            int itemPerPage = 3;
            var halls = service.Search<GamingHallsIndexViewModel>("hall1", itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var hall in halls)
            {
                if (hall.HallName.Contains("hall1"))
                {
                    count++;
                }
            }

            Assert.Equal(1, count);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task SearchByTown()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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
            int page = 1;
            int itemPerPage = 3;
            var halls = service.Search<GamingHallsIndexViewModel>("town", itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var hall in halls)
            {
                if (hall.Town.Contains("town"))
                {
                    count++;
                }
            }

            Assert.Equal(2, count);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task SearchByNevalideString()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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
            int page = 1;
            int itemPerPage = 3;
            var halls = service.Search<IndexGamingHallViewModel>("non-existent", itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var hall in halls)
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
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task All()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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

            int page = 1;
            int itemPerPage = 3;

            var result = service.All<GamingHallsIndexViewModel>(itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var hall in result)
            {
                Assert.Equal("hall" + (count + ((page - 1) * itemPerPage)), hall.HallName);
                count++;
            }

            Assert.Equal(3, count);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllOnTheLastPage()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task AllHallsWithVaildID()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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

            int page = 1;
            int itemPerPage = 3;
            var result = service.AllHalls<GamingHallViewModel>("userId", itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var hall in result)
            {
                count++;
                Assert.Equal("hall" + count, hall.HallName);
            }

            Assert.Equal(itemPerPage, count);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllHallsWithInvaildID()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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

            int page = 1;
            int itemPerPage = 3;
            var result = service.AllHalls<GamingHallViewModel>("userIdInvalid", itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var hall in result)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task AllHallsWithNullID()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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

            int page = 1;
            int itemPerPage = 3;
            var result = service.AllHalls<GamingHallViewModel>(null, itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var hall in result)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task AllOfChainGetAllWithSameName()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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

            var result = service.AllOfChain<GamingHallViewComponentModel>("hall");
            int count = 0;
            foreach (var hall in result)
            {
                count++;
                Assert.Equal("hall", hall.HallName);
            }

            Assert.Equal(5, count);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task AllOfChainGetAllWithNUllName()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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

            var result = service.AllOfChain<GamingHallViewComponentModel>(null);
            int count = 0;
            foreach (var hall in result)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task AllOfChainGetAllWithNoExistingName()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
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

            var result = service.AllOfChain<GamingHallViewComponentModel>("test");
            int count = 0;
            foreach (var hall in result)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetHallCountWithEntity()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);
            await service.AddAsync(
                  "hall1",
                  null,
                  "desc1",
                  "1111",
                  "adress1",
                  "town",
                  "1");
            Assert.Equal(1, service.GetHallsCount("1"));
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public void GetHallCountWithotEntity()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<GamingHall>(dbContext);
            UsersHallsService usersHallsService = new UsersHallsService(new EfRepository<UsersHalls>(dbContext));
            var service = new GamingHallService(repository, usersHallsService);

            Assert.Equal(0, service.GetHallsCount("1"));
            dbContext.Database.EnsureDeleted();
        }
    }
}
