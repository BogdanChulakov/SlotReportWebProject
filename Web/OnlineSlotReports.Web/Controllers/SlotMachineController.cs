namespace OnlineSlotReports.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Services.Data.UsersHallsServices;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;
    using OnlineSlotReports.Web.ViewModels.SlotMachinesViewModels;

    [Authorize]
    public class SlotMachineController : Controller
    {
        private readonly ISlotMachinesService service;
        private readonly IGamingHallService gamingHallService;
        private readonly IUsersHallsService usersHallsService;

        public SlotMachineController(ISlotMachinesService service, IGamingHallService gamingHallService, IUsersHallsService usersHallsService)
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
        public async Task<IActionResult> AddAsync([FromRoute]string id, InputSlotMachineModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.service.AddAsync(input.LicenseNumber, input.Model, input.NumberInHall, id);

            this.TempData["Message"] = "Slot machine was added successfully!";

            return this.Redirect("/SlotMachine/All/" + id);
        }

        public IActionResult All([FromRoute]string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exist = this.usersHallsService.IfExist(id, userId);
            if (hall == null || !exist)
            {
                return this.View("NotFound");
            }

            var macines = this.service.All<SlotMachineViewModel>(id);
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
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            if (hall == null)
            {
                return this.View("NotFound");
            }

            var macines = this.service.All<IndexViewModel>(id);

            var slotMchines = new ViewModels.SlotMachinesViewModels.AllIndexViewModel
            {
                SlotMachines = macines,
                GamingHallId = id,
            };

            return this.View(slotMchines);
        }

        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            string gamingHallid = this.service.GetHallId(id);

            await this.service.DeleteAsync(id);

            this.TempData["Message"] = "Slot machine was deleted successfully!";

            return this.Redirect("/SlotMachine/All/" + gamingHallid);
        }
    }
}
