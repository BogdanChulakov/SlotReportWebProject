namespace OnlineSlotReports.Web.ViewModels.ReportsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllReportsViewModel
    {
        public IEnumerable<IndexReportViewModel> Reports { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string HallId { get; set; }
    }
}
