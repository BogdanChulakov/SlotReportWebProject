namespace OnlineSlotReports.Services.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Data.Repositories;
    using OnlineSlotReports.Services.Data.Tests.Factory;
    using OnlineSlotReports.Services.Data.WinsServices;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using OnlineSlotReports.Web.ViewModels.WinsViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class WinsServiceTests
    {
        [Fact]
        public async Task AddAsyncWithValidlId()
        {
            var dbContextWins = ApplicationDbContextFactory.CreateDbContext();
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
        }

        [Fact]
        public async Task AllWithValidlId()
        {
            var dbContextWins = ApplicationDbContextFactory.CreateDbContext();
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

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var wins = service.All<WinViewModel>("1");
            int count = 0;
            foreach (var win in wins)
            {
                count++;
                Assert.Equal("http://wwww.test.img" + count, win.Url);
                Assert.Equal("desc", win.Description);
                Assert.Equal(date, win.Date);
            }

            Assert.Equal(5, count);
        }

        [Fact]
        public async Task AllWithInvalidlId()
        {
            var dbContextWins = ApplicationDbContextFactory.CreateDbContext();
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

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var wins = service.All<WinViewModel>("2");
            int count = 0;
            foreach (var win in wins)
            {
                count++;
            }

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task AllWithNulllId()
        {
            var dbContextWins = ApplicationDbContextFactory.CreateDbContext();
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

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var pics = service.All<WinViewModel>(null);
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
            var dbContextWins = ApplicationDbContextFactory.CreateDbContext();
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
        }

        [Fact]
        public async Task GetHallIdWithValidId()
        {
            var dbContextWins = ApplicationDbContextFactory.CreateDbContext();
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
        }
    }
}
