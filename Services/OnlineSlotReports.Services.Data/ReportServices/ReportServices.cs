namespace OnlineSlotReports.Services.Data.ReportServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Data.MachineCountersServices;
    using OnlineSlotReports.Services.Mapping;

    public class ReportServices : IReportServices
    {
        private readonly IDeletableEntityRepository<Report> repository;
        private readonly IMachineCountersServices countersServices;

        public ReportServices(IDeletableEntityRepository<Report> repository, IMachineCountersServices countersServices)
        {
            this.repository = repository;
            this.countersServices = countersServices;
        }

        public async Task Add(DateTime date, decimal inForDay, decimal outForday, string hallid)
        {
            var report = new Report
            {
                Date = date,
                InForDay = inForDay,
                OutForDay = outForday,
                GamingHallId = hallid,
            };

            await this.repository.AddAsync(report);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<T> All<T>(string id, string userId)
        {
            IQueryable<Report> halls = this.repository.All().Where(x => x.GamingHallId == id && x.GamingHall.UserId == userId).OrderByDescending(x => x.Date);

            return halls.To<T>().ToList();
        }
    }
}
