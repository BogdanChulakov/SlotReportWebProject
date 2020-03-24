namespace OnlineSlotReports.Services.Data.MachineCountersServices
{
    using System;
    using System.Threading.Tasks;

    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;

    public class MachineCountersServices : IMachineCountersServices
    {
        private readonly IRepository<MachineCounters> repository;

        public MachineCountersServices(IRepository<MachineCounters> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(decimal ellIn, decimal elout, int mechIn, int mechOut, DateTime date, string machineId)
        {
            var counters = new MachineCounters
            {
                ElIn = ellIn,
                ElOut = elout,
                MechIn = mechIn,
                MechOut = mechOut,
                Date = date,
                SlotMachineId = machineId,
            };
            await this.repository.AddAsync(counters);
            await this.repository.SaveChangesAsync();
        }
    }
}
