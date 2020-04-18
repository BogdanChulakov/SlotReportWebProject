namespace OnlineSlotReports.Services.Data.WinsServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class WinsServices : IWinsServices
    {
        private readonly IDeletableEntityRepository<Win> repository;

        public WinsServices(IDeletableEntityRepository<Win> repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(string url, string description, DateTime date, string hallid, string slotMachineId)
        {
            var win = new Win
            {
                Url = url,
                Description = description,
                Date = date,
                GamingHallId = hallid,
                SlotMachineId = slotMachineId,
            };

            await this.repository.AddAsync(win);
            await this.repository.SaveChangesAsync();

            return win.GamingHallId;
        }

        public IEnumerable<T> All<T>(string id)
        {
            IQueryable<Win> wins = this.repository.All().Where(x => x.GamingHallId == id).OrderByDescending(x => x.Date).Take(9);

            return wins.To<T>().ToList();
        }

        public async Task Delete(string id)
        {
            var win = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            this.repository.Delete(win);

            await this.repository.SaveChangesAsync();
        }

        public string GetHallId(string id)
        {
            var win = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            var gamingHallId = win.GamingHallId;

            return gamingHallId;
        }
    }
}
