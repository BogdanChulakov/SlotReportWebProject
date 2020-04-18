namespace OnlineSlotReports.Services.Data.MachineCountersServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class MachineCountersService : IMachineCountersService
    {
        private readonly IRepository<MachineCounters> repository;

        public MachineCountersService(IRepository<MachineCounters> repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(decimal ellIn, decimal elout, int mechIn, int mechOut, DateTime date, string machineId)
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

            string gamingHallid = this.repository
                .All()
                .Where(x => x.SlotMachineId == machineId)
                .Select(x => x.SlotMachine.GamingHallId)
                .FirstOrDefault();

            return gamingHallid;
        }
    }
}
