namespace OnlineSlotReports.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Data.EmployeesServices;
    using OnlineSlotReports.Web.ViewModels.EmployeesViewModel;

    public class EmployeesController : Controller
    {
        private readonly IEmployeesServices services;

        public EmployeesController(IEmployeesServices services)
        {
            this.services = services;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromRoute]string id, InputEmployeesViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            await this.services.AddAsync(input.FullName, input.Email, input.PhoneNumber, input.Password, input.StartWorkDate, id);

            return this.Redirect("/");
        }

        public IActionResult AllEmployees(string id)
        {
            var employees = this.services.All<EmployeeViewModel>(id);
            var allEmployeesViewModel = new AllEmployeesViewModel
            {
                Employees = employees,
                GamingHallId = id,
            };

            return this.View(allEmployeesViewModel);
        }

        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            await this.services.DeleteAsync(id);

            return this.Redirect("/GamingHall/Halls");
        }

        public IActionResult ChangeEmail()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail([FromRoute]string id, EmployeeChangeEmailViewModel model)
        {
            await this.services.ChangeEmailAsync(id, model.Email);

            return this.Redirect("/GamingHall/Halls");
        }

        public IActionResult ChangePhoneNumber()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhoneNumber([FromRoute]string id, ChangePhoneNumberViewModel model)
        {
            await this.services.ChangePhoneNumberAsync(id, model.PhoneNumber);

            return this.Redirect("/GamingHall/Halls");
        }
    }
}
