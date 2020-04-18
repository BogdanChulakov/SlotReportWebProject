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

    public class SlotMachinesService : ISlotMachinesService
    {
        private readonly IDeletableEntityRepository<SlotMachine> repository;

        public SlotMachinesService(IDeletableEntityRepository<SlotMachine> repository)
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
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<T> All<T>(string id)
        {
            IQueryable<SlotMachine> halls = this.repository.All().Where(x => x.GamingHallId == id).OrderBy(x => x.NumberInHall);

            return halls.To<T>().ToList();
        }

        public async Task DeleteAsync(string id)
        {
            var mashine = await this.repository.GetByIdWithDeletedAsync(id);

            this.repository.Delete(mashine);

            await this.repository.SaveChangesAsync();
        }

        public T GetById<T>(string id)
        {
            var machine = this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return machine;
        }

        public string GetHallId(string id)
        {
            var slotMachine = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            var gamingHallId = slotMachine.GamingHallId;

            return gamingHallId;
        }
    }
}
