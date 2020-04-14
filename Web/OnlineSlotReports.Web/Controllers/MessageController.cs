namespace OnlineSlotReports.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineSlotReports.Services.Data.MessageServices;
    using OnlineSlotReports.Web.ViewModels.MessageViewModels;

    public class MessageController : Controller
    {
        private readonly IMessageService massageService;

        public MessageController(IMessageService massageService)
        {
            this.massageService = massageService;
        }

        public IActionResult Create()
        {
            return this.View();
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
            var messages = this.massageService.All<IndexMessageViewModel>(id);
            var allMessages = new AllMessageViewModel();
            allMessages.Messages = messages;

            return this.View(allMessages);
        }

        [Authorize]
        public async Task<IActionResult> Index(string id)
        {
            var message = await this.massageService.GetByIdAsync<IndexMessageViewModel>(id);

            return this.View(message);
        }
    }
}
