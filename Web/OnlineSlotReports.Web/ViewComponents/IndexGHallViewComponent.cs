namespace OnlineSlotReports.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;

    [ViewComponent(Name = "IndexGHall")]
    public class IndexGHallViewComponent : ViewComponent
    {
        private readonly IGamingHallService gamingHallService;

        public IndexGHallViewComponent(IGamingHallService gamingHallService)
        {
            this.gamingHallService = gamingHallService;
        }

        public IViewComponentResult Invoke(string name)
        {
            var gamingHalls = this.gamingHallService.AllOfChain<GamingHallViewComponentModel>(name);

            var halls = new AllGamingHallsViewcomponentModel
            {
                GamingHalls = gamingHalls,
            };

            return this.View(halls);
        }
    }
}
