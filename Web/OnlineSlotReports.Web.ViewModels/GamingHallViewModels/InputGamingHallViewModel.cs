namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class InputGamingHallViewModel
    {
        [Required]
        [MaxLength(50)]
        public string HallName { get; set; }

        public string Description { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string Town { get; set; }
    }
}
