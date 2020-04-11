namespace OnlineSlotReports.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Data.EmployeesServices;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Web.ViewModels.EmployeesViewModel;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;

    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesServices services;
        private readonly IGamingHallService gamingHallService;

        public EmployeesController(IEmployeesServices services, IGamingHallService gamingHallService)
        {
            this.services = services;
            this.gamingHallService = gamingHallService;
        }

        public IActionResult Add([FromRoute]string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (hall == null || userId != hall.UserId)
            {
                return this.NotFound();
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromRoute]string id, InputEmployeesViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.services.AddAsync(input.FullName, input.Email, input.PhoneNumber, input.StartWorkDate, id);

            this.TempData["Message"] = "Employee was added successfully!";

            return this.Redirect("/Employees/AllEmployees/" + id);
        }

        public IActionResult AllEmployees([FromRoute]string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (hall == null || userId != hall.UserId)
            {
                return this.NotFound();
            }

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
            string gamingHallId = await this.services.DeleteAsync(id);

            this.TempData["Message"] = "Employee was deleted successfully!";

            return this.Redirect("/Employees/AllEmployees/" + gamingHallId);
        }

        public IActionResult ChangeEmail([FromRoute]string id)
        {
            var employee = this.services.GetById<EmployeeChangeEmailViewModel>(id);
            if (employee == null)
            {
                return this.NotFound();
            }

            return this.View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail([FromRoute]string id, EmployeeChangeEmailViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string gamingHallId = await this.services.ChangeEmailAsync(id, model.Email);

            this.TempData["Message"] = "Employee email successfully updated!";

            return this.Redirect("/Employees/AllEmployees/" + gamingHallId);
        }

        public IActionResult ChangePhoneNumber([FromRoute]string id)
        {
            var employee = this.services.GetById<ChangePhoneNumberViewModel>(id);
            if (employee == null)
            {
                return this.NotFound();
            }

            return this.View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhoneNumber([FromRoute]string id, ChangePhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string gamingHallId = await this.services.ChangePhoneNumberAsync(id, model.PhoneNumber);

            this.TempData["Message"] = "Employee Phone number successfully updated!";

            return this.Redirect("/Employees/AllEmployees/" + gamingHallId);
        }
    }
}
