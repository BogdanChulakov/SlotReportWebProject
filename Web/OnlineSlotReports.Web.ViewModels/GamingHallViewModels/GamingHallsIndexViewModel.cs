namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class GamingHallsIndexViewModel : IMapFrom<GamingHall>
    {
        public string Id { get; set; }

        public string HallName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public string ShortDescription => 
            this.Description?.Length <= 100 ? this.Description : this.Description?.Substring(0, 100) + "...";

        public string Adress { get; set; }

        public string Town { get; set; }
    }
}
