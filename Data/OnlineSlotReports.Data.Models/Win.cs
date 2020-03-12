namespace OnlineSlotReports.Data.Models
{
    using System;

    using OnlineSlotReports.Data.Common.Models;

    public class Win : BaseDeletableModel<string>
    {
        public Win()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string GamingHallId { get; set; }

        public virtual GamingHall GamingHall { get; set; }

        public string SlotMachineId { get; set; }

        public virtual SlotMachine SlotMachine { get; set; }
    }
}
