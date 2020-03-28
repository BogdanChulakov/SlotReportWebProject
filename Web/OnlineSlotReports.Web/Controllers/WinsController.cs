namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Services.Data.WinsServices;
    using OnlineSlotReports.Web.ViewModels.WinsViewModels;

    public class WinsController : Controller
    {
        private readonly IWinsServices services;
        private readonly ISlotMachinesServices slotMachinesServices;

        public WinsController(IWinsServices services, ISlotMachinesServices slotMachinesServices)
        {
            this.services = services;
            this.slotMachinesServices = slotMachinesServices;
        }

        public IActionResult Add([FromRoute]string id)
        {
            var machineNumbers = this.slotMachinesServices.All<MachineDropDownViewModel>(id);
            InputWinViewModel model = new InputWinViewModel
            {
                MachineNumbers = machineNumbers,
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromRoute]string id, InputWinViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            var gamingHallId = await this.services.AddAsync(input.Url, input.Description, input.Date, id, input.SlotMachineId);

            return this.RedirectToAction("All", "Wins", gamingHallId);
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

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await this.services.Delete(id);

            return this.RedirectToAction("All");
        }
    }
}
