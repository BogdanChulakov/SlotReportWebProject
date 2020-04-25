namespace OnlineSlotReports.Services.Data.GalleryServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGalleryService
    {
        Task DeleteAsync(string id);

        IEnumerable<T> All<T>(string id, int take, int skip = 0);

        string GetHallId(string id);

        Task<string> AddAsync(string url, string hallid);

        int GetGalleryCount(string hallId);

    }
}
