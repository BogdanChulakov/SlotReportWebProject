namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineSlotReports.Data.Common.Models;

    public class GamingHall : BaseDeletableModel<string>
    {
        public GamingHall()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string HallName { get; set; }

        public string Adress { get; set; }

        public string Town { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual IEnumerable<SlotMachine> SlotMachines { get; set; } = new HashSet<SlotMachine>();

        public virtual IEnumerable<Win> Wins { get; set; } = new HashSet<Win>();

        public virtual IEnumerable<Employee> Employees { get; set; } = new HashSet<Employee>();

        public virtual IEnumerable<Pic> Galery { get; set; } = new HashSet<Pic>();
    }
}
