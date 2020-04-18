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
        private readonly ISlotMachinesService service;
        private readonly IGamingHallService gamingHallService;

        public SlotMachineController(ISlotMachinesService service, IGamingHallService gamingHallService)
        {
            this.service = service;
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
        public async Task<IActionResult> AddAsync([FromRoute]string id, InputSlotMachineModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.service.AddAsync(input.LicenseNumber, input.Model, input.NumberInHall, id);

            this.TempData["Message"] = "Successfully added slot machine!";

            return this.Redirect("/SlotMachine/All/" + id);
        }

        public IActionResult All([FromRoute]string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (hall == null || userId != hall.UserId)
            {
                return this.NotFound();
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
                return this.NotFound();
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
