namespace OnlineSlotReports.Services.Data.MachineCountersServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

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

        public IEnumerable<T> All<T>(string id)
        {
            IQueryable<MachineCounters> halls = this.repository.All().Where(x => x.SlotMachine.GamingHallId == id).OrderByDescending(x => x.Date);

            return halls.To<T>().ToList();
        }
    }
}
