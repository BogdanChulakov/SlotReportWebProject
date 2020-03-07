namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineSlotReports.Data.Common.Models;

    public class SlotMachine : BaseDeletableModel<string>
    {
        public string LicenseNumber { get; set; }

        public string Model { get; set; }

        public int NumberInHall { get; set; }

        public string GamingHallId { get; set; }

        public GamingHall GamingHall { get; set; }

        public IEnumerable<MachineCounters> MachinesCounters { get; set; } = new HashSet<MachineCounters>();


        public IEnumerable<Win> Wins { get; set; } = new HashSet<Win>();
    }
}
