namespace OnlineSlotReports.Web.ViewModels.MessageViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class IndexMessageViewModel : IMapFrom<Message>
    {
        public string Id { get; set; }

        public string Sender { get; set; }

        public string Content { get; set; }

        [NotMapped]
        public string ShortContent =>
            this.Content?.Length <= 100 ? this.Content : this.Content?.Substring(0, 100) + "...";

        public DateTime Date { get; set; }

        public bool Readed { get; set; }

        public string GamingHallId { get; set; }
    }
}
