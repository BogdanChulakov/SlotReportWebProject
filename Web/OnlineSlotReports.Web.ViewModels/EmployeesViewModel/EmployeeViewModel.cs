namespace OnlineSlotReports.Web.ViewModels.EmployeesViewModel
{
    using System;

    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Mapping;

    public class EmployeeViewModel : IMapFrom<Employee>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime StartWorkDate { get; set; }

        public string GamingHallId { get; set; }
    }
}
