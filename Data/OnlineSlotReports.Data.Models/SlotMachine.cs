namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineSlotReports.Data.Common.Models;

    public class SlotMachine : BaseDeletableModel<string>
    {
        public SlotMachine()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MinLength(10)]
        [StringLength(10)]
        public string LicenseNumber { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(0, 1000)]
        public int NumberInHall { get; set; }

        [Required]
        public string GamingHallId { get; set; }

        public GamingHall GamingHall { get; set; }

        public virtual IEnumerable<MachineCounters> MachinesCounters { get; set; } = new HashSet<MachineCounters>();

        public virtual IEnumerable<Win> Wins { get; set; } = new HashSet<Win>();
    }
}
