namespace OnlineSlotReports.Web.ViewModels.WinsViewModels
{
    using System;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class IndexWinsViewModel : IMapFrom<Win>
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
