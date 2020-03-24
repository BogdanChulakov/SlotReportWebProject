namespace OnlineSlotReports.Web.ViewModels.WinsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class WinViewModel : IMapFrom<Win>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
