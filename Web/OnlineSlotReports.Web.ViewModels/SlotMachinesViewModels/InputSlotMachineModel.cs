namespace OnlineSlotReports.Web.ViewModels.SlotMachinesViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class InputSlotMachineModel
    {
        [Required]
        [MinLength(10)]
        [StringLength(10)]
        public string LicenseNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Range(0, 1000)]
        public int NumberInHall { get; set; }

        public string GamingHallId { get; set; }
    }
}
