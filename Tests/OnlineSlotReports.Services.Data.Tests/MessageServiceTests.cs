namespace OnlineSlotReports.Services.Data.Tests
{
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Data.Repositories;
    using OnlineSlotReports.Services.Data.MessageServices;
    using OnlineSlotReports.Services.Data.Tests.Factory;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using OnlineSlotReports.Web.ViewModels.MessageViewModels;
    using Xunit;

    public class MessageServiceTests
    {
        [Fact]
        public async Task AddAsyncWithCorectData()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var service = new MessageService(new EfDeletableEntityRepository<Message>(dbContext));
            await service.AddAsync(
                "Ivan Ivanov",
                "content",
                "1");

            var result = await dbContext.Messages.FirstOrDefaultAsync();

            Assert.Equal("Ivan Ivanov", result.Sender);
            Assert.Equal("content", result.Content);
            Assert.Equal("1", result.GamingHallId);
        }

        [Fact]
        public async Task GetByIdAsyncWithCorectId()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var service = new MessageService(new EfDeletableEntityRepository<Message>(dbContext));
            await service.AddAsync(
                "Ivan Ivanov",
                "content",
                "1");

            var message = await dbContext.Messages.FirstOrDefaultAsync();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var result = await service.GetByIdAsync<IndexMessageViewModel>(message.Id);

            Assert.Equal("Ivan Ivanov", result.Sender);
            Assert.Equal("content", result.Content);
            Assert.Equal("1", result.GamingHallId);
        }

        [Fact]
        public async Task AllWithCorectId()
        {
            var dbContext = ApplicationDbContextFactory.CreateDbContext();
            var service = new MessageService(new EfDeletableEntityRepository<Message>(dbContext));
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                "Ivan Ivanov",
                "content",
                "1");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var results = service.All<IndexMessageViewModel>("1");

            int count = 0;
            foreach (var result in results)
            {
                count++;
                Assert.Equal("Ivan Ivanov", result.Sender);
                Assert.Equal("content", result.Content);
                Assert.Equal("1", result.GamingHallId);
            }

            Assert.Equal(5, count);
        }
    }
}
