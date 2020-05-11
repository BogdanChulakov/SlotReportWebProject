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
    using OnlineSlotReports.Services.Data.UsersHallsServices;
    using OnlineSlotReports.Web.ViewModels.EmployeesViewModel;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;

    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService service;
        private readonly IGamingHallService gamingHallService;
        private readonly IUsersHallsService usersHallsService;

        public EmployeesController(IEmployeesService service, IGamingHallService gamingHallService,IUsersHallsService usersHallsService)
        {
            this.service = service;
            this.gamingHallService = gamingHallService;
            this.usersHallsService = usersHallsService;
        }

        public IActionResult Add([FromRoute]string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exist = this.usersHallsService.IfExist(id, userId);
            if (hall == null || !exist)
            {
                return this.View("NotFound");
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

            await this.service.AddAsync(input.FullName, input.Email, input.PhoneNumber, input.StartWorkDate, id);

            this.TempData["Message"] = "Employee was added successfully!";

            return this.Redirect("/Employees/AllEmployees/" + id);
        }

        public IActionResult AllEmployees([FromRoute]string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exist = this.usersHallsService.IfExist(id, userId);
            if (hall == null || !exist)
            {
                return this.View("NotFound");
            }

            var employees = this.service.All<EmployeeViewModel>(id);
            var allEmployeesViewModel = new AllEmployeesViewModel
            {
                Employees = employees,
                GamingHallId = id,
            };

            return this.View(allEmployeesViewModel);
        }

        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            string gamingHallId = this.service.GetHallId(id);

            await this.service.DeleteAsync(id);

            this.TempData["Message"] = "Employee was deleted successfully!";

            return this.Redirect("/Employees/AllEmployees/" + gamingHallId);
        }

        public IActionResult ChangeEmail([FromRoute]string id)
        {
            var employee = this.service.GetById<EmployeeChangeEmailViewModel>(id);
            if (employee == null)
            {
                return this.View("NotFound");
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

            string gamingHallId = this.service.GetHallId(id);

            await this.service.ChangeEmailAsync(id, model.Email);

            this.TempData["Message"] = "Employee email successfully updated!";

            return this.Redirect("/Employees/AllEmployees/" + gamingHallId);
        }

        public IActionResult ChangePhoneNumber([FromRoute]string id)
        {
            var employee = this.service.GetById<ChangePhoneNumberViewModel>(id);
            if (employee == null)
            {
                return this.View("NotFound");
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

            string gamingHallId = this.service.GetHallId(id);

            await this.service.ChangePhoneNumberAsync(id, model.PhoneNumber);

            this.TempData["Message"] = "Employee Phone number successfully updated!";

            return this.Redirect("/Employees/AllEmployees/" + gamingHallId);
        }
    }
}
