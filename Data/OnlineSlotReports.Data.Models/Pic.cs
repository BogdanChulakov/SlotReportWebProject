using OnlineSlotReports.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSlotReports.Data.Models
{
    public class Pic : BaseDeletableModel<string>
    {
        public Pic()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Url { get; set; }

        public string GamingHallId { get; set; }

        public virtual GamingHall GamingHall { get; set; }
    }
}
