namespace OnlineSlotReports.Services.Data.GamingHallServices
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGamingHallService
    {
        Task AddAsync(string hallName, string imageUrl, string description, string phoneNumber, string adress, string town, string userId);

        IEnumerable<T> AllHalls<T>(string userId);

        IEnumerable<T> AllOfChain<T>(string hallName);

        IEnumerable<T> All<T>(int take, int skip = 0);

        Task DeleteAsync(string id);

        T GetT<T>(string id);

        Task UpdateAsync(string id, string hallName, string imageUrl, string description, string phoneNumber, string adress, string town);

        int GetHallsCount();

        int GetSearchHallsCount(string name);

        IEnumerable<T> Search<T>(string name, int take, int skip = 0);
    }
}
