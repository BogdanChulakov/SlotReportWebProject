namespace OnlineSlotReports.Web.ViewModels.WinsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllWinsViewModel
    {
        public int PagesCount { get; set; }

        public int CurentPage { get; set; }

        public IEnumerable<WinViewModel> Wins { get; set; }

        public string GamingHallId { get; set; }
    }
}
