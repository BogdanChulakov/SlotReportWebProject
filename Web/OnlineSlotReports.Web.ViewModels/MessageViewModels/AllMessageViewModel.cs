namespace OnlineSlotReports.Web.ViewModels.MessageViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllMessageViewModel
    {
        public string GamingHallId { get; set; }

        public IEnumerable<IndexMessageViewModel> NewMessages { get; set; }

        public IEnumerable<IndexMessageViewModel> ReadMessages { get; set; }
    }
}
