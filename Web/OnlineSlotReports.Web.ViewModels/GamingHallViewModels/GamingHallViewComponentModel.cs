namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class GamingHallViewComponentModel : IMapFrom<GamingHall>
    {
        public string Id { get; set; }

        public string HallName { get; set; }

        public string Adress { get; set; }

        public string Town { get; set; }
    }
}
