namespace OnlineSlotReports.Services.Data.UsersHallsServices
{
    using System.Threading.Tasks;

    public interface IUsersHallsService
    {
        Task AddHallToUserAsync(string userIf, string hallId);

        bool IfExist(string hallId, string userId);
    }
}
