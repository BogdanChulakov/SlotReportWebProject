namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.ReportServices;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Web.ViewModels.ReportsViewModels;

    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportServices reportServices;

        public ReportsController(IReportServices reportServices)
        {
            this.reportServices = reportServices;
        }

        public IActionResult All([FromRoute] string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reports = this.reportServices.All<IndexReportViewModel>(id, userId);
            var allReports = new AllReportsViewModel
            {
                Reports = reports,
                HallId = id,
            };
            return this.View(allReports);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] string id, InputReportViewModel input)
        {
            await this.reportServices.Add(input.Date, input.InForDay, input.OutForDay, id);

            return this.Redirect("/Reports/All/" + input.GamingHallId);
        }

        public IActionResult ForAPeriod([FromRoute] string id, ForADateReportViewModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reports = this.reportServices.AllForAPeriod<IndexReportViewModel>(id, userId, input.FromDate, input.ToDate);
            var allReports = new ForADateReportViewModel
            {
                Reports = reports,
            };

            return this.View(allReports);
        }
    }
}