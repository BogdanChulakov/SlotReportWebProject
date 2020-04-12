namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class MachineCounters
    {
        public MachineCounters()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal ElIn { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal ElOut { get; set; }

        [Range(0, int.MaxValue)]
        public int MechIn { get; set; }

        [Range(0, int.MaxValue)]
        public int MechOut { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string SlotMachineId { get; set; }

        public virtual SlotMachine SlotMachine { get; set; }
    }
}
