namespace OnlineSlotReports.Services.Data.ReportServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReportServices
    {
        Task Add(DateTime date, decimal inForDay, decimal outForday, string hallid);

        IEnumerable<T> All<T>(string id, string userId);
    }
}
