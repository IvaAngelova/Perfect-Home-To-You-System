namespace PerfectHomeToYou.Services.Apartments
{
    public class ApartmentDetailsServiceModel : ApartmentServiceModel
    {
        public int ClientId { get; init; }

        public string ClientName { get; init; }

        public string UserId { get; init; }
    }
}
