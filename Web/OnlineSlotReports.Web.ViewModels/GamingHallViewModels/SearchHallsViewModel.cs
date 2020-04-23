namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SearchHallsViewModel
    {
        public string Name { get; set; }

        public int PagesCount { get; set; }

        public int CurentPage { get; set; }

        public IEnumerable<GamingHallsIndexViewModel> GamingHalls { get; set; }
    }
}
