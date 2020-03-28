namespace OnlineSlotReports.Web.ViewModels.ReportsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class IndexReportViewModel : IMapFrom<SlotMachine>
    {
        public string Id { get; set; }

        public string LicenseNumber { get; set; }

        public string Model { get; set; }

        public int NumberInHall { get; set; }


    }
}
