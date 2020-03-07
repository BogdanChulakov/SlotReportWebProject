using OnlineSlotReports.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSlotReports.Data.Models
{
    public class Win : BaseDeletableModel<string>
    {

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public GamingHall GamingHall { get; set; }

        public string GamingHallid { get; set; }


        public SlotMachine SlotMachine { get; set; }

        public string SlotMachineId { get; set; }
    }
}
