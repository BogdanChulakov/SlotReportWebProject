namespace OnlineSlotReports.Services.Data.SlotMachinesServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISlotMachinesService
    {
        IEnumerable<T> All<T>(string id);

        Task AddAsync(string licenseNumber, string model, int numberInHall, string gamingHallId);

        Task DeleteAsync(string id);

        string GetHallId(string id);

        T GetById<T>(string id);
    }
}
