namespace OnlineSlotReports.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Web.ViewModels.SlotMachinesViewModels;

    public class SlotMachineController : Controller
    {
        private readonly ISlotMachinesServices services;

        public SlotMachineController(ISlotMachinesServices services)
        {
            this.services = services;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(InputSlotMachineModel input)
        {
            await this.services.AddAsync(input.LicenseNumber, input.Model, input.NumberInHall, "fyh");
            return this.Redirect("SlotMachine/All");
        }

        public IActionResult All(string hallId)
        {
            var macines = this.services.All<SlotMachineViewModel>(hallId);
            var slotMchines = new AllSlotMachinesViewModel
            {
                SlotMachines = macines,
            };

            return this.View(slotMchines);
        }
    }
}
