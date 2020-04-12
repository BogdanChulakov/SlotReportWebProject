namespace OnlineSlotReports.Web.ViewModels.MachineCounters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class InputMachineCountersViewModel : IMapFrom<MachineCounters>
    {
        [Range(0, 9999999999999999.99)]
        public decimal EllIn { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal EllOut { get; set; }

        [Range(0, int.MaxValue)]
        public int MechIn { get; set; }

        [Range(0, int.MaxValue)]
        public int MechOut { get; set; }

        public DateTime Date { get; set; }
    }
}
