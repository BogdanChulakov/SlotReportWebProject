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
        public decimal EllInForDay { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal EllOutForDay { get; set; }

        public decimal EllResultForDay => this.EllInForDay - this.EllOutForDay;

        [Required]
        public int MechInForDay { get; set; }

        [Required]
        public int MechOutForDay { get; set; }

        public int MechResultForDay => this.MechInForDay - this.MechOutForDay;

        [Required]
        public string SlotMachineId { get; set; }

        public SlotMachine SlotMachine { get; set; }

        [Required]
        public string GamingHallId { get; set; }

        public GamingHall GamingHall { get; set; }
    }
}
