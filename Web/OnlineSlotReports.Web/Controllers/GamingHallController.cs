namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Common;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Services.Data.UsersHallsServices;
    using OnlineSlotReports.Web.CloudinaryHelper;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;

    [Authorize]
    public class GamingHallController : Controller
    {
        private const int ItemPerPage = GlobalConstants.ItemPerPage;

        private readonly IGamingHallService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;
        private readonly IUsersHallsService usersHallsService;

        public GamingHallController(IGamingHallService service, UserManager<ApplicationUser> userManager, Cloudinary cloudinary, IUsersHallsService usersHallsService)
        {
            this.service = service;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
            this.usersHallsService = usersHallsService;
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
                return this.View(input);
            }

            if (file != null)
            {
                input.ImageUrl = await CloudinaryExtension.UploadAsync(this.cloudinary, file);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.service.AddAsync(input.HallName, input.ImageUrl, input.Description, input.PhoneNumber, input.Adress, input.Town, userId);
            this.TempData["Message"] = "Gaming hall was aded successfully!";
            return this.Redirect("/GamingHall/Halls");
        }

        public async Task<IActionResult> Halls(int page = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            AllHallsViewModel allHallsViewModel = new AllHallsViewModel
            {
                GamingHalls = this.service.AllHalls<GamingHallViewModel>(user.Id, GlobalConstants.ItemPerPageHalls, (page - 1) * GlobalConstants.ItemPerPageHalls),
            };
            var count = this.service.GetHallsCount(user.Id);
            allHallsViewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemPerPageHalls);
            allHallsViewModel.CurentPage = page;
            return this.View(allHallsViewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.service.DeleteAsync(id);

            this.TempData["Message"] = "Gaming hall was deleted successfully!";
            return this.Redirect("/GamingHall/Halls");
        }

        public IActionResult Update(string id)
        {
            var hall = this.service.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exist = this.usersHallsService.IfExist(id, userId);

            if (hall == null || !exist)
            {
                return this.View("NotFound");
            }

            var datailsViewModel = this.service.GetT<UpdateHallViewModel>(id);
            return this.View(datailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateHallViewModel input, IFormFile file)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (file != null)
            {
                input.ImageUrl = await CloudinaryExtension.UploadAsync(this.cloudinary, file);
            }

            await this.service.UpdateAsync(input.Id, input.HallName, input.ImageUrl, input.Description, input.PhoneNumber, input.Adress, input.Town);
            this.TempData["Message"] = "Gaming hall was edited successfully!";
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

        [AllowAnonymous]
        [HttpGet("/GamingHall/Search/")]
        public IActionResult Search(string name, int page = 1)
        {
            var searchModel = new SearchHallsViewModel();
            var halls = this.service.Search<GamingHallsIndexViewModel>(name, ItemPerPage, (page - 1) * ItemPerPage);
            searchModel.GamingHalls = halls;
            if (name != null && halls.Count() == 0)
            {
               this.TempData["message"] = "No result for this serach!";
            }

            var count = this.service.GetSearchHallsCount(name);
            searchModel.PagesCount = (int)Math.Ceiling((double)count / ItemPerPage);
            searchModel.CurentPage = page;
            searchModel.Name = name;
            return this.View(searchModel);
        }

        [AllowAnonymous]
        public IActionResult All(int page = 1)
        {
            AllIndexHallViewModel allHallsViewModel = new AllIndexHallViewModel
            {
                GamingHalls = this.service.All<GamingHallsIndexViewModel>(ItemPerPage, (page - 1) * ItemPerPage),
            };

            var count = this.service.GetAllHallsCount();
            allHallsViewModel.PagesCount = (int)Math.Ceiling((double)count / ItemPerPage);
            allHallsViewModel.CurentPage = page;
            return this.View(allHallsViewModel);
        }

        [AllowAnonymous]
        public IActionResult Index([FromRoute] string id)
        {
            var model = this.service.GetT<IndexGamingHallViewModel>(id);
            if (model == null)
            {
                return this.View("NotFound");
            }

            return this.View(model);
        }

        [AllowAnonymous]
        public IActionResult Details([FromRoute]string id)
        {
            var datailsViewModel = this.service.GetT<DetailsViewModel>(id);
            if (datailsViewModel == null)
            {
                return this.View("NotFound");
            }

            return this.View(datailsViewModel);
        }
    }
}
