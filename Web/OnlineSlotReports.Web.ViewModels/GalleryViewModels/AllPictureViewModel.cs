namespace OnlineSlotReports.Web.ViewModels.GalleryViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllPictureViewModel
    {
        public IEnumerable<PictureViewModel> Pictures { get; set; }

        public string GamingHallId { get; set; }
    }
}
