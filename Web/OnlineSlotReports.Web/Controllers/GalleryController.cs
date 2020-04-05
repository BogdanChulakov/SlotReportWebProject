﻿namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.GalleryServices;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Web.CloudinaryHelper;
    using OnlineSlotReports.Web.ViewModels.GalleryViewModels;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;

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
            if (id == null)
            {
                id = this.TempData["id"].ToString();
            }

            var pictures = this.services.All<PictureViewModel>(id);

            var allpicture = new AllPictureViewModel
            {
                Pictures = pictures,
                GamingHallId = id,
            };

            return this.View("View", allpicture);
        }

        public IActionResult Index([FromRoute] string id)
        {
            if (id == null)
            {
                id = this.TempData["id"].ToString();
            }

            var pictures = this.services.All<PictureViewModel>(id);

            var allpicture = new AllPictureViewModel
            {
                Pictures = pictures,
                GamingHallId = id,
            };

            return this.View(allpicture);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromRoute] string id, IFormFile file)
        {
            string url = await CloudinaryExtension.UploadAsync(this.cloudinary, file);

            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            await this.services.AddAsync(url, id);

            this.TempData["id"] = id;

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var gamingHallId = this.services.GetHallId(id);
            this.TempData["id"] = gamingHallId;

            var hall = this.gamingHallService.GetT<DetailsViewModel>(gamingHallId);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (hall.UserId != userId)
            {
                return this.Content("Gredichka haker!");
            }

            await this.services.Delete(id);
            return this.RedirectToAction("All");
        }
    }
}
