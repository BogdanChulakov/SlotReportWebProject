namespace OnlineSlotReports.Services.Data.ReportServices
{
    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ReportServices : IReportServices
    {
        private readonly IDeletableEntityRepository<Report> repository;

        public ReportServices(IDeletableEntityRepository<Report> repository)
        {
            this.repository = repository;
        }

        public async Task Add(string id, DateTime date)
        {
            
        }
    }
}
