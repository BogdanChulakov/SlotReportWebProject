namespace OnlineSlotReports.Web.ViewModels.EmployeesViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ChangePhoneNumberViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
}
