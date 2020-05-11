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
    using OnlineSlotReports.Services.Data.UsersHallsServices;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;
    using OnlineSlotReports.Web.ViewModels.ReportsViewModels;

    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportService reportServices;
        private readonly IGamingHallService gamingHallService;
        private readonly IUsersHallsService usersHallsService;

        public ReportsController(IReportService reportServices, IGamingHallService gamingHallService, IUsersHallsService usersHallsService)
        {
            this.reportServices = reportServices;
            this.gamingHallService = gamingHallService;
            this.usersHallsService = usersHallsService;
        }

        public IActionResult All([FromRoute] string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exist = this.usersHallsService.IfExist(id, userId);
            if (hall == null || !exist)
            {
                return this.View("NotFound");
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
            var exist = this.usersHallsService.IfExist(id, userId);
            if (hall == null || !exist)
            {
                return this.View("NotFound");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] string id, InputReportViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.reportServices.AddAsync(input.Date, input.InForDay, input.OutForDay, id);

            this.TempData["Message"] = "Report was added successfully!";

            return this.Redirect("/Reports/All/" + id);
        }

        [HttpGet("/Report/ForAPeriod/")]
        public IActionResult ForAPeriod([FromQuery] string id, DateTime fromDate, DateTime toDate)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exist = this.usersHallsService.IfExist(id, userId);

            if (hall == null || !exist)
            {
                return this.View("NotFound");
            }

            var reports = this.reportServices.AllForAPeriod<IndexReportViewModel>(id, fromDate, toDate);
            var allReports = new ForADateReportViewModel
            {
                Reports = reports,
            };

            return this.View(allReports);
        }
    }
}