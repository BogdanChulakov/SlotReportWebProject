namespace OnlineSlotReports.Services.Data.WinsServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IWinsServices
    {
        IEnumerable<T> All<T>(string id);
    }
}
