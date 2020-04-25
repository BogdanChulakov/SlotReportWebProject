namespace OnlineSlotReports.Services.Data.GalleryServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class GalleryService : IGalleryService
    {
        private readonly IDeletableEntityRepository<Pic> repository;

        public GalleryService(IDeletableEntityRepository<Pic> repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(string url, string hallid)
        {
            var pic = new Pic
            {
                Url = url,
                GamingHallId = hallid,
            };
            await this.repository.AddAsync(pic);

            await this.repository.SaveChangesAsync();

            return pic.Id;
        }

        public IEnumerable<T> All<T>(string id, int take, int skip = 0)
        {
            IQueryable<Pic> allPicture = this.repository.All().Where(x => x.GamingHallId == id).Skip(skip).Take(take);

            return allPicture.To<T>().ToList();
        }

        public async Task DeleteAsync(string id)
        {
            var pic = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            this.repository.Delete(pic);

            await this.repository.SaveChangesAsync();
        }

        public int GetGalleryCount(string hallId)
        {
            int count = this.repository.All().Where(x => x.GamingHallId == hallId).Count();

            return count;
        }

        public string GetHallId(string id)
        {
            var pic = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            var gamingHallId = pic.GamingHallId;

            return gamingHallId;
        }
    }
}
