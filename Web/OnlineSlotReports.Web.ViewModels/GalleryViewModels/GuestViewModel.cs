namespace OnlineSlotReports.Web.ViewModels.GalleryViewModels
{
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class GuestViewModel : IMapFrom<Pic>
    {
        public string Id { get; set; }

        public string Url { get; set; }
    }
}
