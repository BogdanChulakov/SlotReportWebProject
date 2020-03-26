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

    public class GalleryServices : IGalleryServices
    {
        private readonly IDeletableEntityRepository<Pic> repository;

        public GalleryServices(IDeletableEntityRepository<Pic> repository)
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

        public IEnumerable<T> All<T>(string id)
        {
            IQueryable<Pic> allPicture = this.repository.All().Where(x => x.GamingHallId == id);

            return allPicture.To<T>().ToList();
        }

        public async Task Delete(string id)
        {
            var pic = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            this.repository.Delete(pic);

            await this.repository.SaveChangesAsync();
        }
    }
}
