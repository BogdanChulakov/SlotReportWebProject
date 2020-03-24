namespace OnlineSlotReports.Web.ViewModels.EmployeesViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllEmployeesViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public string GamingHallId { get; set; }
    }
}
