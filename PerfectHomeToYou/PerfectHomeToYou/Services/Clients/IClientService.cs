namespace PerfectHomeToYou.Services.Clients
{
    public interface IClientService
    {
        public bool IsClient(string userId);

        public int IdByUser(string userId);
    }
}
