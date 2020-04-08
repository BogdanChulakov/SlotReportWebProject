namespace OnlineSlotReports.Web.ViewModels.SlotMachinesViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllIndexViewModel
    {
        public IEnumerable<IndexViewModel> SlotMachines { get; set; }

        public string GamingHallId { get; set; }
    }
}
