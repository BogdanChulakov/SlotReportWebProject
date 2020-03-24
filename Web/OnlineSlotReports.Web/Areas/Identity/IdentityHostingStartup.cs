using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(OnlineSlotReports.Web.Areas.Identity.IdentityHostingStartup))]

namespace OnlineSlotReports.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
