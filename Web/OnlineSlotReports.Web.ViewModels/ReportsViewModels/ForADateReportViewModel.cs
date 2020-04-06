namespace OnlineSlotReports.Web.ViewModels.ReportsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ForADateReportViewModel
    {
        public string Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public IEnumerable<IndexReportViewModel> Reports { get; set; }

        public decimal TotalIn => this.Reports.Sum(x => x.InForDay);

        public decimal TotalOut => this.Reports.Sum(x => x.OutForDay);

        public decimal TotaResult => this.Reports.Sum(x => x.DeilyResult);
    }
}
