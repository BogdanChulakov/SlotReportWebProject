namespace OnlineSlotReports.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;

    public class GamingHallController : Controller
    {
        private readonly IGamingHallService service;
        private readonly UserManager<ApplicationUser> userManager;

        public GamingHallController(IGamingHallService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(InputGamingHallViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.service.AddAsync(input.HallName,input.Description,input.PhoneNumber, input.Adress, input.Town, userId);

            return this.Redirect("/");
        }

        public async Task<IActionResult> Halls()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            AllHallsViewModel allHallsViewModel = new AllHallsViewModel
            {
                GamingHalls = this.service.AllHalls<GamingHallViewModel>(user.Id),
            };

            return this.View(allHallsViewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.service.DeleteAsync(id);

            return this.Redirect("/GamingHall/Halls");
        }

        public IActionResult Details(string id)
        {
            var datailsViewModel = this.service.GetT<DetailsViewModel>(id);

            return this.View(datailsViewModel);
        }

        public IActionResult Update(string id)
        {
            var datailsViewModel = this.service.GetT<DetailsViewModel>(id);
            return this.View(datailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DetailsViewModel input)
        {
            await this.service.UpdateAsync(input.Id, input.HallName, input.Description, input.PhoneNumber, input.Adress, input.Town);

            return this.Redirect("/GamingHall/Halls");
        }

        public IActionResult AddElements([FromQuery] string id)
        {
            var hallView = new AddElementsViewModel
            {
                Id = id,
            };
            return this.View(hallView);
        }
    }
}
