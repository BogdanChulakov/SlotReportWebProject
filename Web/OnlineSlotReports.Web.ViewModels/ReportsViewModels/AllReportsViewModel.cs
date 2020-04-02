namespace OnlineSlotReports.Web.ViewModels.ReportsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllReportsViewModel
    {
       public IEnumerable<IndexReportViewModel> Reports { get; set; }
    }
}
