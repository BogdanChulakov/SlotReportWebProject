namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        private readonly IGalleryService services;
        private readonly IGamingHallService gamingHallService;
        private readonly Cloudinary cloudinary;

        public GalleryController(
            IGalleryService services,
            IGamingHallService gamingHallService,
            Cloudinary cloudinary)
        {
            this.services = services;
            this.gamingHallService = gamingHallService;
            this.cloudinary = cloudinary;
        }

        public IActionResult All([FromRoute] string id, int page = 1)
        {
            var pictures = this.services.All<PictureViewModel>(id, GlobalConstants.ItemPerPageGallery, (page - 1) * GlobalConstants.ItemPerPageGallery);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            if (hall == null)
            {
                return this.View("NotFound");
            }

            foreach (var item in pictures)
            {
                if (item.GamingHallUserId != userId && !this.User.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    return this.View("NotFound");
                }
            }

            var allpicture = new AllPictureViewModel
            {
                Pictures = pictures,
                GamingHallId = id,
            };
            var count = this.services.GetGalleryCount(id);
            allpicture.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemPerPageGallery);
            allpicture.CurentPage = page;
            allpicture.GamingHallId = id;
            return this.View("View", allpicture);
        }

        [AllowAnonymous]
        public IActionResult Index([FromRoute] string id, int page = 1)
        {
            var pictures = this.services.All<GuestViewModel>(id, GlobalConstants.ItemPerPageGallery, (page - 1) * GlobalConstants.ItemPerPageGallery);
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            if (hall == null)
            {
                return this.View("NotFound");
            }

            var allpicture = new AllGuestViewModel
            {
                Pictures = pictures,
                GamingHallId = id,
            };
            var count = this.services.GetGalleryCount(id);
            allpicture.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemPerPageGallery);
            allpicture.CurentPage = page;
            allpicture.GamingHallId = id;
            return this.View(allpicture);
        }

        public IActionResult Add([FromRoute] string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (hall == null || userId != hall.UserId)
            {
                return this.View("NotFound");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromRoute] string id, ICollection<IFormFile> files)
        {
            if (files.Count == 0)
            {
                return this.View();
            }

            var urls = await CloudinaryExtension.UploadManyAsync(this.cloudinary, files);
            foreach (var url in urls)
            {
                await this.services.AddAsync(url, id);
            }

            this.TempData["Message"] = "Pictures was added successfully!";

            return this.Redirect("/Gallery/All/" + id);
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var gamingHallId = this.services.GetHallId(id);

            await this.services.DeleteAsync(id);

            this.TempData["Message"] = "Picture was deleted successfully!";

            return this.Redirect("/Gallery/All/" + gamingHallId);
        }
    }
}
