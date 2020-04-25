namespace OnlineSlotReports.Services.Data.WinsServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IWinsService
    {
        IEnumerable<T> All<T>(string id, int take, int skip = 0);

        int GetWinsCount(string hallId);

        Task<string> AddAsync(string url, string description, DateTime date, string hallid, string slotMachineId);

        Task DeleteAsync(string id);

        string GetHallId(string id);
    }
}
