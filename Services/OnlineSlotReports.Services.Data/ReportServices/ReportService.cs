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

    public class ReportService : IReportService
    {
        private readonly IDeletableEntityRepository<Report> repository;
        private readonly IMachineCountersService countersServices;

        public ReportService(IDeletableEntityRepository<Report> repository, IMachineCountersService countersServices)
        {
            this.repository = repository;
            this.countersServices = countersServices;
        }

        public async Task AddAsync(DateTime date, decimal inForDay, decimal outForday, string hallid)
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

        public IEnumerable<T> All<T>(string id)
        {
            IQueryable<Report> halls = this.repository.All().Where(x => x.GamingHallId == id).OrderByDescending(x => x.Date);

            return halls.To<T>().ToList();
        }

        public IEnumerable<T> AllForAPeriod<T>(string id, DateTime fromData, DateTime toData)
        {
            IQueryable<Report> reports = this.repository.All().Where(x => x.GamingHallId == id
                                                                     && x.Date >= fromData
                                                                     && x.Date <= toData).OrderByDescending(x => x.Date);

            return reports.To<T>().ToList();
        }

    }
}
