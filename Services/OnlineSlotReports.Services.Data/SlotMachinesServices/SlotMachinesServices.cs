namespace OnlineSlotReports.Services.Data.SlotMachinesServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class SlotMachinesServices : ISlotMachinesServices
    {
        private readonly IDeletableEntityRepository<SlotMachine> repository;

        public SlotMachinesServices(IDeletableEntityRepository<SlotMachine> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(string licenseNumber, string model, int numberInHall, string gamingHallId)
        {
            var slotMachine = new SlotMachine
            {
                LicenseNumber = licenseNumber,
                Model = model,
                NumberInHall = numberInHall,
                GamingHallId = gamingHallId,
            };

            await this.repository.AddAsync(slotMachine);
        }

        public IEnumerable<T> All<T>(string id)
        {
            IQueryable<SlotMachine> halls = this.repository.All().Where(x => x.GamingHallId == id);

            return halls.To<T>().ToList();
        }
    }
}
