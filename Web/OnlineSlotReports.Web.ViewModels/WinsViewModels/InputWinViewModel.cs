namespace OnlineSlotReports.Web.ViewModels.WinsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class InputWinViewModel
    {

        public string Url { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string GamingHallId { get; set; }

        [Required]
        public string SlotMachineId { get; set; }

        public IEnumerable<MachineDropDownViewModel> MachineNumbers { get; set; }
    }
}
