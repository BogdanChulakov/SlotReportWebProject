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

            await this.services.AddAsync(input.EllIn, input.EllOut, input.MechIn, input.MechOut, date, id);

            return this.Redirect("/GamingHall/Halls");
        }
    }
}
