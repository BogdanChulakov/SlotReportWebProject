namespace OnlineSlotReports.Web.ViewModels.EmployeesViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InputEmployeesViewModel
    {
        [Required]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public DateTime StartWorkDate { get; set; }
    }
}
