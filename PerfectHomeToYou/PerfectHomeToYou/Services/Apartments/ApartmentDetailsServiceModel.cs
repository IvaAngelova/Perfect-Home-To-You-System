namespace PerfectHomeToYou.Services.Apartments
{
    public class ApartmentDetailsServiceModel : ApartmentServiceModel
    {
        public int ClientId { get; init; }

        public string ClientName { get; init; }

        public int CityId { get; set; }

        public string NeighborhoodName { get; set; }

        public string UserId { get; init; }
    }
}
