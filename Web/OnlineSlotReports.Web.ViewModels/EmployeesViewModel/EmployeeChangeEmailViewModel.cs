namespace OnlineSlotReports.Web.ViewModels.EmployeesViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class EmployeeChangeEmailViewModel : IMapFrom<Employee>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
