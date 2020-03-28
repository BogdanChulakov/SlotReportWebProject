namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Web.ViewModels.ReportsViewModels;

    public class ReportsController : Controller
    {
        private readonly ISlotMachinesServices slotMachines;

        public ReportsController(ISlotMachinesServices slotMachines)
        {
            this.slotMachines = slotMachines;
        }

        public IActionResult Index([FromRoute] string id)
        {
            this.TempData["hallId"] = id;
            var machines = this.slotMachines.All<IndexReportViewModel>(id);
            var model = new AllMachinesViewModel();
            model.MachinesViewModels = machines;
            return this.View(model);
        }

        public IActionResult Add()
        {
            return this.View();
        }
    }
}
