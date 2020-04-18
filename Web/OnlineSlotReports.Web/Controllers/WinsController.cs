namespace OnlineSlotReports.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Common;
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
        private readonly ISlotMachinesService slotMachinesServices;
        private readonly Cloudinary cloudinary;
        private readonly IGamingHallService gamingHallService;

        public WinsController(
            IWinsServices services,
            ISlotMachinesService slotMachinesServices,
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
            if (hall == null || userId != hall.UserId)
            {
                return this.NotFound();
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
                return this.View(input);
            }

            input.Url = await CloudinaryExtension.UploadAsync(this.cloudinary, file);

            var gamingHallId = await this.services.AddAsync(input.Url, input.Description, input.Date, id, input.SlotMachineId);

            this.TempData["Message"] = "Object was added successfully!";

            return this.Redirect("/Wins/All/" + gamingHallId);
        }

        public IActionResult All([FromRoute] string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (hall == null || (userId != hall.UserId && !this.User.IsInRole(GlobalConstants.AdministratorRoleName)))
            {
                return this.NotFound();
            }

            var wins = this.services.All<WinViewModel>(id);
            var allWins = new AllWinsViewModel
            {
                Wins = wins,
                GamingHallId = id,
            };

            return this.View(allWins);
        }

        [AllowAnonymous]
        public IActionResult Index([FromRoute] string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            if (hall == null)
            {
                return this.NotFound();
            }

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

            string gamingHallId = this.services.GetHallId(id);

            await this.services.Delete(id);

            this.TempData["Message"] = "Object was deleted successfully!";

            return this.Redirect("/Wins/All/" + gamingHallId);
        }
    }
}
