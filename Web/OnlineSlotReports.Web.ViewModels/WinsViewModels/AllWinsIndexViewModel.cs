namespace OnlineSlotReports.Web.ViewModels.WinsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllWinsIndexViewModel
    {
        public int PagesCount { get; set; }

        public int CurentPage { get; set; }

        public IEnumerable<IndexWinsViewModel> Wins { get; set; }

        public string GamingHallId { get; set; }
    }
}
