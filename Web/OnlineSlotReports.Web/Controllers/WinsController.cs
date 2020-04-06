namespace OnlineSlotReports.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Services.Data.WinsServices;
    using OnlineSlotReports.Web.CloudinaryHelper;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;
    using OnlineSlotReports.Web.ViewModels.WinsViewModels;

    [Authorize]
    public class WinsController : Controller
    {
        private readonly IWinsServices services;
        private readonly ISlotMachinesServices slotMachinesServices;
        private readonly Cloudinary cloudinary;
        private readonly IGamingHallService gamingHallService;

        public WinsController(
            IWinsServices services,
            ISlotMachinesServices slotMachinesServices,
            Cloudinary cloudinary,
            IGamingHallService gamingHallService)
        {
            this.services = services;
            this.slotMachinesServices = slotMachinesServices;
            this.cloudinary = cloudinary;
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

            var machineNumbers = this.slotMachinesServices.All<MachineDropDownViewModel>(id);
            InputWinViewModel model = new InputWinViewModel
            {
                MachineNumbers = machineNumbers,
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromRoute]string id, InputWinViewModel input, IFormFile file)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            input.Url = await CloudinaryExtension.UploadAsync(this.cloudinary, file);

            var gamingHallId = await this.services.AddAsync(input.Url, input.Description, input.Date, id, input.SlotMachineId);

            return this.Redirect("/Wins/All/" + gamingHallId);
        }

        public IActionResult All([FromRoute] string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != hall.UserId)
            {
                return this.Redirect("/GamingHall/Halls");
            }

            var wins = this.services.All<WinViewModel>(id);
            var allWins = new AllWinsViewModel
            {
                Wins = wins,
                GamingHallId = id,
            };
            if (id == null)
            {
                return this.View("Error");
            }

            return this.View(allWins);
        }

        [AllowAnonymous]
        public IActionResult Index([FromRoute] string id)
        {
            var wins = this.services.All<WinViewModel>(id);
            var allWins = new AllWinsViewModel
            {
                Wins = wins,
                GamingHallId = id,
            };
            return this.View(allWins);
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.services.Delete(id);

            return this.RedirectToAction("All");
        }
    }
}
