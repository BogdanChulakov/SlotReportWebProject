namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllIndexHallViewModel
    {
        public int PagesCount { get; set; }

        public int CurentPage { get; set; }
        public IEnumerable<GamingHallsIndexViewModel> GamingHalls { get; set; }
    }
}
