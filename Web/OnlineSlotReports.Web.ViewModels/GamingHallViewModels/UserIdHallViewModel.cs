namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class UserIdHallViewModel : IMapFrom<GamingHall>
    {
        public string Id { get; set; }

        public string UserId { get; set; }
    }
}
