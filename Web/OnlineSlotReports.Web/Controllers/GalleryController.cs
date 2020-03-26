namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.GalleryServices;
    using OnlineSlotReports.Web.ViewModels.GalleryViewModels;

    public class GalleryController : Controller
    {
        private readonly IGalleryServices services;

        public GalleryController(IGalleryServices services)
        {
            this.services = services;
        }

        public IActionResult All([FromQuery] string id)
        {
            var pictures = this.services.All<PictureViewModel>(id);

            var allpicture = new AllPictureViewModel
            {
                Pictures = pictures,
                GamingHallId = id,
            };

            return this.View("View", allpicture);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromRoute] string id, InputPicViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            await this.services.AddAsync(input.Url, id);

            return this.RedirectToAction("All", "Gallery", id);
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await this.services.Delete(id);

            return this.RedirectToAction("All");
        }
    }
}
