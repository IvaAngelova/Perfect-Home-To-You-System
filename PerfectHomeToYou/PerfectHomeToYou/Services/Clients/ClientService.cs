using System.Linq;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Data.Models;

namespace PerfectHomeToYou.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly PerfectHomeToYouDbContext context;

        public ClientService(PerfectHomeToYouDbContext context)
            => this.context = context;

        public int BecomeClient(string firstName, string lastName, string phoneNumber, string userId)
        {
            var clientData = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                UserId = userId
            };

            this.context.Clients.Add(clientData);
            this.context.SaveChanges();

            return clientData.Id;
        }

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
