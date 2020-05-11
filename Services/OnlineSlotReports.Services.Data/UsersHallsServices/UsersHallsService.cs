namespace OnlineSlotReports.Services.Data.UsersHallsServices
{
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;

    public class UsersHallsService : IUsersHallsService
    {
        private readonly IRepository<UsersHalls> repository;

        public UsersHallsService(IRepository<UsersHalls> repository)
        {
            this.repository = repository;
        }

        public async Task AddHallToUserAsync(string userId, string hallId)
        {
            var usersHalls = new UsersHalls
            {
                UserId = userId,
                GamingHallId = hallId,
            };
            await this.repository.AddAsync(usersHalls);
            await this.repository.SaveChangesAsync();
        }

        public bool IfExist(string hallId, string userId)
        {
            var userHall = this.repository.All().Where(x => x.GamingHallId == hallId && x.UserId == userId).FirstOrDefault();

            if (userHall == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
