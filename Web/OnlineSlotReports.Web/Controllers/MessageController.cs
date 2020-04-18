namespace OnlineSlotReports.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Services.Data.MessageServices;
    using OnlineSlotReports.Web.ViewModels.GamingHallViewModels;
    using OnlineSlotReports.Web.ViewModels.MessageViewModels;

    public class MessageController : Controller
    {
        private readonly IMessageService massageService;
        private readonly IGamingHallService gamingHallService;

        public MessageController(IMessageService massageService, IGamingHallService gamingHallService)
        {
            this.massageService = massageService;
            this.gamingHallService = gamingHallService;
        }

        public IActionResult Create(string id)
        {
            var model = new InputMessageViewModel();
            model.GamingHallId = id;
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string id, InputMessageViewModel input)
        {
            await this.massageService.AddAsync(input.Sender, input.Content, id);

            this.TempData["message"] = "message successfully sent";

            return this.Redirect("/GamingHall/Index/" + id);
        }

        [Authorize]
        public IActionResult All(string id)
        {
            var hall = this.gamingHallService.GetT<UserIdHallViewModel>(id);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (hall == null || userId != hall.UserId)
            {
                return this.NotFound();
            }

            var newMessages = this.massageService.All<IndexMessageViewModel>(id);
            var readMessages = this.massageService.GetAllReadById<IndexMessageViewModel>(id);

            var allMessages = new AllMessageViewModel();
            allMessages.NewMessages = newMessages;
            allMessages.ReadMessages = readMessages;
            return this.View(allMessages);
        }

        [Authorize]
        public async Task<IActionResult> Index(string id)
        {
            var message = await this.massageService.GetByIdAsync<IndexMessageViewModel>(id);
            if (message == null)
            {
                return this.NotFound();
            }

            return this.View(message);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var hallId = this.massageService.GetHallId(id);

            await this.massageService.DeleteAsync(id);

            return this.Redirect("/Message/All/" + hallId);
        }
    }
}
