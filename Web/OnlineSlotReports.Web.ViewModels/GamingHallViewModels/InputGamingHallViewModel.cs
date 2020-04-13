namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class InputGamingHallViewModel
    {
        [Required]
        [MaxLength(50)]
        public string HallName { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Phone number can contains only digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string Town { get; set; }
    }
}
