namespace OnlineSlotReports.Web.ViewModels.WinsViewModels
{
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class MachineDropDownViewModel : IMapFrom<SlotMachine>
    {
        public string Id { get; set; }

        public int NumberInHall { get; set; }
    }
}