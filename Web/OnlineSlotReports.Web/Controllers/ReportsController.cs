namespace OnlineSlotReports.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Services.Data.ReportServices;
    using OnlineSlotReports.Services.Data.SlotMachinesServices;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;
    using OnlineSlotReports.Web.ViewModels.ReportsViewModels;

    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportServices reportServices;
        private readonly IGamingHallService gamingHallService;

        public ReportsController(IReportServices reportServices, IGamingHallService gamingHallService)
        {
            this.reportServices = reportServices;
            this.gamingHallService = gamingHallService;
        }

        public IActionResult All([FromRoute] string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != hall.UserId)
            {
                return this.Redirect("/GamingHall/Halls");
            }

            var reports = this.reportServices.All<IndexReportViewModel>(id);
            var allReports = new AllReportsViewModel
            {
                Reports = reports,
                HallId = id,
            };
            return this.View(allReports);
        }

        public IActionResult Add([FromRoute]string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != hall.UserId)
            {
                return this.Redirect("/GamingHall/Halls");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] string id, InputReportViewModel input)
        {
            await this.reportServices.Add(input.Date, input.InForDay, input.OutForDay, id);

            this.TempData["Message"] = "REport was added successfully!";

            return this.Redirect("/Reports/All/" + input.GamingHallId);
        }

        [HttpGet("/Report/ForAPeriod/")]
        public IActionResult ForAPeriod([FromQuery] ForADateReportViewModel input)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(input.Id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != hall.UserId)
            {
                return this.Redirect("/GamingHall/Halls");
            }

            var reports = this.reportServices.AllForAPeriod<IndexReportViewModel>(input.Id, userId, input.FromDate, input.ToDate);
            var allReports = new ForADateReportViewModel
            {
                Reports = reports,
            };

            return this.View(allReports);
        }
    }
}