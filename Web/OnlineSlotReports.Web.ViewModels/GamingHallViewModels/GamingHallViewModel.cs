namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class GamingHallViewModel : IMapFrom<GamingHall>
    {
        public string Id { get; set; }

        public string HallName { get; set; }

        public string Adress { get; set; }

        public string Town { get; set; }

        public int SlotmachineCount { get; set; }
    }
}
