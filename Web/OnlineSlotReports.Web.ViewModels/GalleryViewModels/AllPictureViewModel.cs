namespace OnlineSlotReports.Web.ViewModels.GalleryViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllPictureViewModel
    {
        public int PagesCount { get; set; }

        public int CurentPage { get; set; }

        public string GamingHallId { get; set; }

        public IEnumerable<PictureViewModel> Pictures { get; set; }
    }
}
