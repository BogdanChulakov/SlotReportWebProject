namespace OnlineSlotReports.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;
    using OnlineSlotReports.Web.ViewModels.SlotMachinesViewModels;

    [Authorize]
    public class SlotMachineController : Controller
    {
        private readonly ISlotMachinesServices services;
        private readonly IGamingHallService gamingHallService;

        public SlotMachineController(ISlotMachinesServices services, IGamingHallService gamingHallService)
        {
            this.services = services;
            this.gamingHallService = gamingHallService;
        }

        public IActionResult Add([FromRoute]string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != hall.UserId)
            {
                return this.Redirect("/GamingHall/Halls");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromRoute]string id, InputSlotMachineModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            await this.services.AddAsync(input.LicenseNumber, input.Model, input.NumberInHall, id);
            return this.Redirect("/GamingHall/Halls");
        }

        public IActionResult All([FromRoute]string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != hall.UserId)
            {
                return this.Redirect("/GamingHall/Halls");
            }

            var macines = this.services.All<SlotMachineViewModel>(id);
            var slotMchines = new AllSlotMachinesViewModel
            {
                SlotMachines = macines,
                GamingHallId = id,
            };

            return this.View(slotMchines);
        }

        [AllowAnonymous]
        public IActionResult Index([FromRoute]string id)
        {
            var macines = this.services.All<IndexViewModel>(id);
            var slotMchines = new AllSlotMachinesIndexViewModel
            {
                SlotMachines = macines,
            };

            return this.View(slotMchines);
        }

        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            await this.services.DeleteAsync(id);

            return this.Redirect("/GamingHall/Halls");
        }
    }
}
