namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using OnlineSlotReports.Data.Common.Models;

    public class Message : BaseDeletableModel<string>
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Sender { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public bool Readed { get; set; }

        [Required]
        public string GamingHallId { get; set; }

        public GamingHall GamingHall { get; set; }
    }
}
