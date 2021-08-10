namespace PerfectHomeToYou.Services.Apartments.Models
{
    public class ApartmentDetailsServiceModel : ApartmentServiceModel
    {
        public int ClientId { get; init; }

        public string ClientName { get; init; }

        public int CityId { get; set; }

        public string UserId { get; init; }
    }
}
