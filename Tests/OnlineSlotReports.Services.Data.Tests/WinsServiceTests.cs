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
    using OnlineSlotReports.Services.Data.Tests.Factory;
    using OnlineSlotReports.Services.Data.WinsServices;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using OnlineSlotReports.Web.ViewModels.WinsViewModels;
    using Xunit;

    public class WinsServiceTests
    {
        [Fact]
        public async Task AddAsyncWithValidlId()
        {
            ApplicationDbContext dbContextWins = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new WinsService(new EfDeletableEntityRepository<Win>(dbContextWins));
            var date = DateTime.UtcNow;
            await service.AddAsync( 
                "http://wwww.test.img",
                "desc",
                date,
                "1",
                "1");

            var win = await dbContextWins.Wins.FirstOrDefaultAsync();

            Assert.Equal("http://wwww.test.img", win.Url);
            Assert.Equal("desc", win.Description);
            Assert.Equal(date, win.Date);
            Assert.Equal("1", win.GamingHallId);

            dbContextWins.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllWithValidlId()
        {
            ApplicationDbContext dbContextWins = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new WinsService(new EfDeletableEntityRepository<Win>(dbContextWins));
            var date = DateTime.UtcNow;
            for (int i = 1; i < 6; i++)
            {
                await service.AddAsync(
               "http://wwww.test.img" + i,
               "desc",
               date,
               "1",
               "1");
            }

            int page = 1;
            int itemPerPage = 3;

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var wins = service.All<WinViewModel>("1", itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var win in wins)
            {
                count++;
                Assert.Equal("http://wwww.test.img" + count, win.Url);
                Assert.Equal("desc", win.Description);
                Assert.Equal(date, win.Date);
            }

            Assert.Equal(itemPerPage, count);
            dbContextWins.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllWithInvalidlId()
        {
            ApplicationDbContext dbContextWins = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new WinsService(new EfDeletableEntityRepository<Win>(dbContextWins));
            var date = DateTime.UtcNow;
            for (int i = 1; i < 6; i++)
            {
                await service.AddAsync(
               "http://wwww.test.img" + i,
               "desc",
               date,
               "1",
               "1");
            }

            int page = 1;
            int itemPerPage = 3;

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var wins = service.All<WinViewModel>("2", itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var win in wins)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContextWins.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllWithNulllId()
        {
            ApplicationDbContext dbContextWins = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new WinsService(new EfDeletableEntityRepository<Win>(dbContextWins));
            var date = DateTime.UtcNow;
            for (int i = 1; i < 6; i++)
            {
                await service.AddAsync(
               "http://wwww.test.img" + i,
               "desc",
               date,
               "1",
               "1");
            }

            int page = 1;
            int itemPerPage = 3;
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var pics = service.All<WinViewModel>(null, itemPerPage, (page - 1) * itemPerPage);
            int count = 0;
            foreach (var pic in pics)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContextWins.Database.EnsureDeleted();
        }

        [Fact]
        public async Task DeleteAsyncWithValidId()
        {
            ApplicationDbContext dbContextWins = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new WinsService(new EfDeletableEntityRepository<Win>(dbContextWins));
            var date = DateTime.UtcNow;
            await service.AddAsync(
               "http://wwww.test.img",
               "desc",
               date,
               "1",
               "1");

            var win = await dbContextWins.Wins.FirstOrDefaultAsync();

            await service.DeleteAsync(win.Id);

            var result = await dbContextWins.Wins.Where(x => x.Id == win.Id).FirstOrDefaultAsync();

            Assert.True(result == null);
            dbContextWins.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetHallIdWithValidId()
        {
            ApplicationDbContext dbContextWins = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new WinsService(new EfDeletableEntityRepository<Win>(dbContextWins));
            var date = DateTime.UtcNow;
            await service.AddAsync(
               "http://wwww.test.img",
               "desc",
               date,
               "1",
               "1");

            var win = await dbContextWins.Wins.FirstOrDefaultAsync();

            string hallId = service.GetHallId(win.Id);

            Assert.Equal("1", hallId);
            dbContextWins.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetGalleryCountWithEntity()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<Win>(dbContext);
            var service = new WinsService(repository);
            var date = DateTime.UtcNow;
            await service.AddAsync(
               "http://wwww.test.img",
               "desc",
               date,
               "1",
               "1");
            Assert.Equal(1, service.GetWinsCount("1"));
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetGalleryCountWithNoexistingName()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var repository = new EfDeletableEntityRepository<Win>(dbContext);
            var service = new WinsService(repository);
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "http://wwww.test.img",
                "desc",
                date,
                "1",
                "1");
            Assert.Equal(0, service.GetWinsCount("11"));
            dbContext.Database.EnsureDeleted();
        }
    }
}
