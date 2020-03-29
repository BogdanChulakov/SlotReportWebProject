namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineSlotReports.Data.Common.Models;

    public class Report : BaseDeletableModel<string>
    {
        public Report()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime Date { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal InForDay { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal OutForDay { get; set; }

        [Required]
        public string GamingHallId { get; set; }

        public GamingHall GamingHall { get; set; }
    }
}
