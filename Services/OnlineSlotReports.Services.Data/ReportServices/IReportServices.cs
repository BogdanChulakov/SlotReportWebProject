namespace OnlineSlotReports.Services.Data.ReportServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReportServices
    {
        Task Add(string id, DateTime date);
    }
}
