namespace OnlineSlotReports.Web.ViewModels.ReportsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class IndexReportViewModel : IMapFrom<Report>
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public decimal InForDay { get; set; }

        public decimal OutForDay { get; set; }

        public decimal DeilyResult => this.InForDay - this.OutForDay;

        public string GamingHallId { get; set; }
    }
}
