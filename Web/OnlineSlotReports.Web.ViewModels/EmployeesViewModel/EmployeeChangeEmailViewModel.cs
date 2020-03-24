namespace OnlineSlotReports.Web.ViewModels.EmployeesViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class EmployeeChangeEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
