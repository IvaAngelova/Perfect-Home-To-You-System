namespace PerfectHomeToYou.Services.Clients
{
    public interface IClientService
    {
        public int IdByUser(string userId);
     
        public bool IsClient(string userId);
    }
}
