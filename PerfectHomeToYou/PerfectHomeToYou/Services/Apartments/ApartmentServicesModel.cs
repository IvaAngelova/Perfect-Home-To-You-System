namespace PerfectHomeToYou.Services.Apartments
{
    public class ApartmentServicesModel
    {
        public int Id { get; init; }

        public string ApartmentType { get; set; }

        public string City { get; init; }

        public string Neighborhood { get; init; }

        public int Floor { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string RentOrSell { get; set; }
    }
}
