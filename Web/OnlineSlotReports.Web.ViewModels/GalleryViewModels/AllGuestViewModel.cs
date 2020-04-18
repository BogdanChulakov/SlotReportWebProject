namespace OnlineSlotReports.Web.ViewModels.GalleryViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllGuestViewModel
    {
        public IEnumerable<GuestViewModel> Pictures { get; set; }

        public string GamingHallId { get; set; }
    }
}
