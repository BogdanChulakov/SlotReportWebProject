namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.MachineCountersServices;
    using OnlineSlotReports.Web.ViewModels.MachineCounters;

    public class CountersController : Controller
    {
        private readonly IMachineCountersServices services;

        public CountersController(IMachineCountersServices services)
        {
            this.services = services;
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
                return this.Content("Ivalid Input!");
            }

            var date = DateTime.UtcNow;

            string gamingHallid = await this.services.AddAsync(input.EllIn, input.EllOut, input.MechIn, input.MechOut, date, id);

            this.TempData["Message"] = "Machine counters was added successfully!";

            return this.Redirect("/SlotMachine/All/" + gamingHallid);
        }
    }
}
