namespace OnlineSlotReports.Services.Data.SlotMachinesServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISlotMachinesServices
    {
        IEnumerable<T> All<T>(string id);

        Task AddAsync(string licenseNumber, string model, int numberInHall, string gamingHallId);

        Task<string> DeleteAsync(string id);
    }
}
