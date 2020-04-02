namespace OnlineSlotReports.Web.ViewModels.SlotMachinesViewModels
{
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class IndexViewModel : IMapFrom<SlotMachine>
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public int NumberInHall { get; set; }
    }
}
