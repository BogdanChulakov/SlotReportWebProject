namespace OnlineSlotReports.Web.ViewModels.ReportsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class InputReportViewModel
    {
        public DateTime Date { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal InForDay { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal OutForDay { get; set; }

        public string GamingHallId { get; set; }
    }
}
