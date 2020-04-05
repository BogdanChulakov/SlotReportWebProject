namespace OnlineSlotReports.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Web.CloudinaryHelper;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;

    [Authorize]
    public class GamingHallController : Controller
    {
        private readonly IGamingHallService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public GamingHallController(IGamingHallService service, UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
        {
            this.service = service;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(InputGamingHallViewModel input, IFormFile file)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            if (file != null)
            {
                input.ImageUrl = await CloudinaryExtension.UploadAsync(this.cloudinary, file);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.service.AddAsync(input.HallName, input.ImageUrl, input.Description, input.PhoneNumber, input.Adress, input.Town, userId);

            return this.Redirect("/GamingHall/Halls");
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

        [AllowAnonymous]
        public IActionResult All()
        {
            AllIndexViewModel allHallsViewModel = new AllIndexViewModel
            {
                GamingHalls = this.service.All<GamingHallsIndexViewModel>(),
            };

            return this.View(allHallsViewModel);
        }

        [AllowAnonymous]
        public IActionResult Index([FromRoute] string id)
        {
            var model = this.service.GetT<IndexGamingHallViewModel>(id);
            return this.View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.service.DeleteAsync(id);

            return this.Redirect("/GamingHall/Halls");
        }

        [AllowAnonymous]
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
        public async Task<IActionResult> Update(DetailsViewModel input, IFormFile file)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            if (file != null)
            {
                input.ImageUrl = await CloudinaryExtension.UploadAsync(this.cloudinary, file);
            }

            await this.service.UpdateAsync(input.Id, input.HallName, input.ImageUrl, input.Description, input.PhoneNumber, input.Adress, input.Town);

            return this.Redirect("/GamingHall/Halls");
        }

        public IActionResult AddElements([FromRoute] string id)
        {
            var hallView = new AddElementsViewModel
            {
                Id = id,
            };
            return this.View(hallView);
        }
    }
}
