namespace OnlineSlotReports.Services.Data.MachineCountersServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMachineCountersServices
    {
        Task AddAsync(decimal ellIn, decimal elout, int mechIn, int mechOut, DateTime date, string machineId);

        IEnumerable<T> All<T>(string id);
    }
}
