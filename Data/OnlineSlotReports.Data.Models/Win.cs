namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using OnlineSlotReports.Data.Common.Models;

    public class Win : BaseDeletableModel<string>
    {
        public Win()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string GamingHallId { get; set; }

        public virtual GamingHall GamingHall { get; set; }

        [Required]
        public string SlotMachineId { get; set; }

        public virtual SlotMachine SlotMachine { get; set; }
    }
}
