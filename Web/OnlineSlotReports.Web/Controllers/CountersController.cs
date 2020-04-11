namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.MachineCountersServices;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Web.ViewModels.MachineCounters;
    using OnlineSlotReports.Web.ViewModels.SlotMachinesViewModels;

    [Authorize]
    public class CountersController : Controller
    {
        private readonly IMachineCountersServices services;
        private readonly ISlotMachinesServices slotMachinesServices;

        public CountersController(IMachineCountersServices services, ISlotMachinesServices slotMachinesServices)
        {
            this.services = services;
            this.slotMachinesServices = slotMachinesServices;
        }

        public IActionResult Enter()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Enter([FromRoute]string id, InputMachineCountersViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var slotMachine = this.slotMachinesServices.GetById<IndexViewModel>(id);

            if (slotMachine == null)
            {
                return this.NotFound();
            }

            var date = DateTime.UtcNow;

            string gamingHallid = await this.services.AddAsync(input.EllIn, input.EllOut, input.MechIn, input.MechOut, date, id);

            this.TempData["Message"] = "Machine counters was added successfully!";

            return this.Redirect("/SlotMachine/All/" + gamingHallid);
        }
    }
}
