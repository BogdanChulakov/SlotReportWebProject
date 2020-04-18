namespace OnlineSlotReports.Services.Data.WinsServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IWinsServices
    {
        IEnumerable<T> All<T>(string id);

        Task<string> AddAsync(string url, string description, DateTime date, string hallid, string slotMachineId);

        Task Delete(string id);

        string GetHallId(string id);
    }
}
