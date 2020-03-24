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
        public async Task<IActionResult> AddAsync([FromRoute]string id, InputSlotMachineModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Content("Ivalid Input!");
            }

            await this.services.AddAsync(input.LicenseNumber, input.Model, input.NumberInHall, id);
            return this.Redirect("/GamingHall/Halls");
        }

        public IActionResult All([FromQuery]string id)
        {
            var macines = this.services.All<SlotMachineViewModel>(id);
            var slotMchines = new AllSlotMachinesViewModel
            {
                SlotMachines = macines,
                GamingHallId = id,
            };

            return this.View(slotMchines);
        }

        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            await this.services.DeleteAsync(id);

            return this.Redirect("/GamingHall/Halls");
        }
    }
}
