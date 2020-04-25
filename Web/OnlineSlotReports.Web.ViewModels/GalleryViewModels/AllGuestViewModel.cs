namespace OnlineSlotReports.Web.ViewModels.GalleryViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllGuestViewModel
    {
        public int PagesCount { get; set; }

        public int CurentPage { get; set; }

        public string GamingHallId { get; set; }

        public IEnumerable<GuestViewModel> Pictures { get; set; }
    }
}
