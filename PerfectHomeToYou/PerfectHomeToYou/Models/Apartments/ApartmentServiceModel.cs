namespace PerfectHomeToYou.Models.Apartments
{
    public class ApartmentServiceModel
    {
        public int Id { get; init; }

        public string ApartmentType { get; set; }

        public string City { get; init; }

        public string Neighborhood { get; init; }

        public int Floor { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string RentOrSell { get; set; }
    }
}