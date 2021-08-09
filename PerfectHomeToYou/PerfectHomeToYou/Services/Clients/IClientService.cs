namespace PerfectHomeToYou.Services.Clients
{
    public interface IClientService
    {
        public int BecomeClient(string firstName, string lastName, string phoneNumber, string userId);

        public int IdByUser(string userId);
     
        public bool IsClient(string userId);
    }
}
