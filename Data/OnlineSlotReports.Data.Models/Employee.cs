namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineSlotReports.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class Employee : BaseDeletableModel<string>
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime StartWorkDate { get; set; }

        public DateTime? EndWorkDate { get; set; }

        public GamingHall GamingHall { get; set; }

        public string GamingHallId { get; set; }
    }
}
