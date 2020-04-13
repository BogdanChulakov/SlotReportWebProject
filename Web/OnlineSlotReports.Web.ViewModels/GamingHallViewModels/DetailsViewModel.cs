namespace OnlineSlotReports.Web.ViewModels.GamingHallViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class DetailsViewModel : IMapFrom<GamingHall>
    {
        public string Id { get; set; }

        [Required]
        public string HallName { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Phone number can contains only digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string Town { get; set; }

        public string UserId { get; set; }
    }
}
