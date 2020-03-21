namespace OnlineSlotReports.Services.Data.GamingHallServices
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class GamingHallService : IGamingHallService
    {
        private readonly IDeletableEntityRepository<GamingHall> repository;

        public GamingHallService(IDeletableEntityRepository<GamingHall> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(string hallName, string adress, string town, string userId)
        {
            var gaminhHall = new GamingHall
            {
                HallName = hallName,
                Adress = adress,
                Town = town,
                UserId = userId,
            };
            await this.repository.AddAsync(gaminhHall);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<T> AllHalls<T>(string userId)
        {
            IQueryable<GamingHall> halls = this.repository.All().Where(x => x.UserId == userId).OrderBy(x => x.CreatedOn);

            return halls.To<T>().ToList();
        }

        public async Task DeleteAsync(string id)
        {
            var hall = this.repository.All().Where(x => x.Id == id).FirstOrDefault();
            this.repository.Delete(hall);
            await this.repository.SaveChangesAsync();
        }

        public T GetT<T>(string id)
        {
            var hall = this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return hall;
        }

        public async Task UpdateAsync(string id, string hallName, string adress, string town)
        {
            var hall = this.repository.All().Where(x => x.Id == id).FirstOrDefault();
            hall.HallName = hallName;
            hall.Adress = adress;
            hall.Town = town;
            await this.repository.SaveChangesAsync();
        }
    }
}
