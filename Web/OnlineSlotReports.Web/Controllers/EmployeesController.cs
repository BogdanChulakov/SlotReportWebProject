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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public EmployeesController(IEmployeesServices services, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.services = services;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

            await this.userManager.CreateAsync(new ApplicationUser{UserName = input.Email, Email = input.Email, EmailConfirmed = true, }, input.Password);

            var role = await this.roleManager.FindByNameAsync("Croupier");

            if (role == null)
            {
                await this.roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Croupier",
                });
            }

            var user = await this.userManager.FindByNameAsync(input.Email);

            await this.userManager.AddToRoleAsync(user, "Croupier");

            return this.Redirect("/");
        }

        public IActionResult AllEmployees([FromRoute]string id)
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
