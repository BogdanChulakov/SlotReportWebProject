namespace OnlineSlotReports.Web.ViewModels.GalleryViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class InputPicViewModel
    {
        [Url]
        [Required]
        public string Url { get; set; }

        public string GamingHallId { get; set; }
    }
}
