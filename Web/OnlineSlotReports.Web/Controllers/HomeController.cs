namespace OnlineSlotReports.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Web.ViewModels;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            //if (this.User.IsInRole("Croupier"))
            //{
            //    return this.Content("Implement report!!!!!!!!!!!!!!!!!!!!");
            //}

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
