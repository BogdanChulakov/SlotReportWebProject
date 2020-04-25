namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllHallsViewModel
    {
        public int PagesCount { get; set; }

        public int CurentPage { get; set; }

        public IEnumerable<GamingHallViewModel> GamingHalls { get; set; }
    }
}
