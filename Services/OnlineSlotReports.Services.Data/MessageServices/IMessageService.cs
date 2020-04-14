namespace OnlineSlotReports.Services.Data.MessageServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        IEnumerable<T> All<T>(string id);

        Task<T> GetByIdAsync<T>(string id);

        Task AddAsync(string hallId, string sender, string content);
    }
}
