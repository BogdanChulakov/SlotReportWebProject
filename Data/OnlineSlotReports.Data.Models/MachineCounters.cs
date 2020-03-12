using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSlotReports.Data.Models
{
    public class MachineCounters
    {
        public MachineCounters()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public decimal ElIn { get; set; }

        public decimal ElOut { get; set; }

        public int MechjIn { get; set; }

        public int MechOut { get; set; }

        public DateTime Date { get; set; }

        public string SlotMachineId { get; set; }

        public virtual SlotMachine SlotMachine { get; set; }
    }
}
