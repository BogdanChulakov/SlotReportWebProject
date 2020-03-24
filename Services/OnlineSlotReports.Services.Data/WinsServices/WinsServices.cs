namespace OnlineSlotReports.Services.Data.WinsServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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

        public IEnumerable<T> All<T>(string id)
        {
            IQueryable<Win> wins = this.repository.All().Where(x => x.GamingHallId == id).OrderByDescending(x => x.Date);

            return wins.To<T>().ToList();
        }
    }
}
