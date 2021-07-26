using System.Linq;

using PerfectHomeToYou.Data;

namespace PerfectHomeToYou.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly PerfectHomeToYouDbContext context;

        public ClientService(PerfectHomeToYouDbContext context)
            => this.context = context;

        public int IdByUser(string userId)
            => this.context
                .Clients
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefault();

        public bool IsClient(string userId)
            => this.context
                   .Clients
                   .Any(c => c.UserId == userId);
    }
}
