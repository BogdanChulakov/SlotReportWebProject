namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineSlotReports.Data.Common.Models;

    public class Pic : BaseDeletableModel<string>
    {
        public Pic()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Url]
        [Required]
        public string Url { get; set; }

        [Required]
        public string GamingHallId { get; set; }

        public virtual GamingHall GamingHall { get; set; }
    }
}
