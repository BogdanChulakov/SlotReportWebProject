namespace OnlineSlotReports.Services.Data.MessageServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineSlotReports.Common;
    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class MessageService : IMessageService
    {
        private readonly IDeletableEntityRepository<Message> repository;

        public MessageService(IDeletableEntityRepository<Message> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(string sender, string content, string hallId)
        {
            if (sender == null)
            {
                sender = GlobalConstants.AnonymousMessage;
            }

            var message = new Message
            {
                Sender = sender,
                Content = content,
                Date = DateTime.UtcNow,
                GamingHallId = hallId,
            };

            await this.repository.AddAsync(message);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<T> All<T>(string id)
        {
            IQueryable<Message> employees = this.repository.All().Where(x => x.GamingHallId == id && x.Readed == false).OrderByDescending(x => x.Date);

            return employees.To<T>().ToList();
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var messageT = this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            var message = this.repository.All().Where(x => x.Id == id).FirstOrDefault();
            message.Readed = true;
            await this.repository.SaveChangesAsync();

            return messageT;
        }
    }
}
