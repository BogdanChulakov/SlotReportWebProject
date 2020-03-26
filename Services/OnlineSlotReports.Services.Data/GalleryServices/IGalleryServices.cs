namespace OnlineSlotReports.Services.Data.GalleryServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGalleryServices
    {

        Task Delete(string id);

        IEnumerable<T> All<T>(string id);

        Task<string> AddAsync(string url, string hallid);
    }
}
