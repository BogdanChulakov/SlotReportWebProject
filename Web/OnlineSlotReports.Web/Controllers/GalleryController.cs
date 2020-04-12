namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Common;
    using OnlineSlotReports.Services.Data.GalleryServices;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Web.CloudinaryHelper;
    using OnlineSlotReports.Web.ViewModels.GalleryViewModels;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;

    [Authorize]
    public class GalleryController : Controller
    {
        private readonly IGalleryServices services;
        private readonly IGamingHallService gamingHallService;
        private readonly Cloudinary cloudinary;

        public GalleryController(
            IGalleryServices services,
            IGamingHallService gamingHallService,
            Cloudinary cloudinary)
        {
            this.services = services;
            this.gamingHallService = gamingHallService;
            this.cloudinary = cloudinary;
        }

        public IActionResult All([FromRoute] string id)
        {
            var pictures = this.services.All<PictureViewModel>(id);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            if (hall == null)
            {
                return this.NotFound();
            }
            foreach (var item in pictures)
            {
                if (item.GamingHallUserId != userId && !this.User.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    return this.NotFound();
                }
            }

            var allpicture = new AllPictureViewModel
            {
                Pictures = pictures,
                GamingHallId = id,
            };

            return this.View("View", allpicture);
        }

        [AllowAnonymous]
        public IActionResult Index([FromRoute] string id)
        {
            var pictures = this.services.All<PictureViewModel>(id);
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            if (hall == null)
            {
                return this.NotFound();
            }

            var allpicture = new AllPictureViewModel
            {
                Pictures = pictures,
                GamingHallId = id,
            };

            return this.View(allpicture);
        }

        public IActionResult Add([FromRoute] string id)
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
        public async Task<IActionResult> AddAsync([FromRoute] string id, IFormFile file)
        {
            if (file == null)
            {
                return this.View();
            }

            string url = await CloudinaryExtension.UploadAsync(this.cloudinary, file);

            await this.services.AddAsync(url, id);

            this.TempData["Message"] = "Picture was added successfully!";

            return this.Redirect("/Gallery/All/" + id);
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var gamingHallId = this.services.GetHallId(id);

            await this.services.Delete(id);

            this.TempData["Message"] = "Picture was deleted successfully!";

            return this.Redirect("/Gallery/All/" + gamingHallId);
        }
    }
}
