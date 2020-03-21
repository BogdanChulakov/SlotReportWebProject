namespace OnlineSlotReports.Web.ViewModels.SlotMachinesViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class SlotMachineViewModel : IMapFrom<SlotMachine>
    {
        [Required]
        public string LicenseNumber { get; set; }

        [Required]
        public string Model { get; set; }

        public int NumberInHall { get; set; }

        [Required]
        public string GamingHallId { get; set; }
    }
}
