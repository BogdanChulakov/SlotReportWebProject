namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.WinsServices;
    using OnlineSlotReports.Web.ViewModels.WinsViewModels;

    public class WinsController : Controller
    {
        private readonly IWinsServices services;

        public WinsController(IWinsServices services)
        {
            this.services = services;
        }

        public IActionResult All([FromRoute] string id)
        {
            var wins = this.services.All<WinViewModel>(id);
            var allWins = new AllWinsViewModel
            {
                Wins = wins,
                GamingHallId = id,
            };

            return this.View(allWins);
        }
    }
}
