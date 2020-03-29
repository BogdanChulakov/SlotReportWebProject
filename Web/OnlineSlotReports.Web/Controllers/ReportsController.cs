namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.ReportServices;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Web.ViewModels.ReportsViewModels;

    public class ReportsController : Controller
    {
        private readonly IReportServices reportServices;

        public ReportsController(IReportServices reportServices)
        {
            this.reportServices = reportServices;
        }

        public IActionResult All([FromRoute] string id)
        {
            this.TempData["hallId"] = id;
            var reports = this.reportServices.All<IndexReportViewModel>(id);
            var allReports = new AllReportsViewModel
            {
                Reports = reports,
            };
            return this.View(allReports);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(InputReportViewModel input)
        {
            await this.reportServices.Add(input.Date, input.InForDay, input.OutForDay, this.TempData["hallId"].ToString());

            return this.Redirect("/Reports/All/" + this.TempData["hallId"].ToString());
        }
    }
}
