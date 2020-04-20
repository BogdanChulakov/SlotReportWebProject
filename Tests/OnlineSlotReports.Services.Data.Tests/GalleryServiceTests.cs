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
    using OnlineSlotReports.Services.Data.GalleryServices;
    using OnlineSlotReports.Services.Data.Tests.Factory;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using OnlineSlotReports.Web.ViewModels.GalleryViewModels;
    using Xunit;

    public class GalleryServiceTests
    {

        [Fact]
        public async Task AddAsyncWithValidlId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new GalleryService(new EfDeletableEntityRepository<Pic>(dbContext));
            await service.AddAsync(
                "http://wwww.test.img",
                "1");

            var pic = await dbContext.Pics.FirstOrDefaultAsync();

            Assert.Equal("http://wwww.test.img", pic.Url);
            Assert.Equal("1", pic.GamingHallId);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AllWithValidlId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new GalleryService(new EfDeletableEntityRepository<Pic>(dbContext));
            for (int i = 1; i < 6; i++)
            {
                await service.AddAsync(
                               "http://wwww.test.img" + i,
                               "1");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var pics = service.All<GuestViewModel>("1");
            int count = 0;
            foreach (var pic in pics)
            {
                count++;
                Assert.Equal("http://wwww.test.img" + count, pic.Url);
            }

            Assert.Equal(5, count);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task AllWithInvalidlId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new GalleryService(new EfDeletableEntityRepository<Pic>(dbContext));
            for (int i = 1; i < 6; i++)
            {
                await service.AddAsync(
                               "http://wwww.test.img" + i,
                               "1");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var pics = service.All<GuestViewModel>("2");
            int count = 0;
            foreach (var pic in pics)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task AllWithNulllId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new GalleryService(new EfDeletableEntityRepository<Pic>(dbContext));
            for (int i = 1; i < 6; i++)
            {
                await service.AddAsync(
                               "http://wwww.test.img" + i,
                               "1");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var pics = service.All<GuestViewModel>(null);
            int count = 0;
            foreach (var pic in pics)
            {
                count++;
            }

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task DeleteAsyncWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new GalleryService(new EfDeletableEntityRepository<Pic>(dbContext));
            await service.AddAsync(
                "http://www.test.com",
                "1");

            var pic = await dbContext.Pics.FirstOrDefaultAsync();

            await service.DeleteAsync(pic.Id);

            var result = await dbContext.Pics.Where(x => x.Id == pic.Id).FirstOrDefaultAsync();

            Assert.True(result == null);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task GetHallIdWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new GalleryService(new EfDeletableEntityRepository<Pic>(dbContext));
            await service.AddAsync(
                "http://www.test.com",
                "1");

            var pic = await dbContext.Pics.FirstOrDefaultAsync();

            string hallId = service.GetHallId(pic.Id);

            Assert.Equal("1", hallId);
            dbContext.Database.EnsureDeleted();

        }
    }
}
