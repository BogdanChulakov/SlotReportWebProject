namespace OnlineSlotReports.Data.Models
{
    using OnlineSlotReports.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
