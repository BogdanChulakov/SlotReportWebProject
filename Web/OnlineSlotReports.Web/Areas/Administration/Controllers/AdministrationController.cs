namespace OnlineSlotReports.Web.Areas.Administration.Controllers
{
    using OnlineSlotReports.Common;
    using OnlineSlotReports.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
