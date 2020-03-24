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

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime StartWorkDate { get; set; }
    }
}
