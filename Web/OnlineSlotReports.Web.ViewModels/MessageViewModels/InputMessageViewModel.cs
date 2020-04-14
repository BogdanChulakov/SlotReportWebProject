namespace OnlineSlotReports.Web.ViewModels.MessageViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class InputMessageViewModel
    {
        public string Sender { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
