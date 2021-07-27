namespace PerfectHomeToYou.Models.Home
{
    public class ApartmentIndexViewModel
    {
        public int Id { get; init; }

        public string City { get; init; }

        public string Neighborhood { get; init; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string RentOrSale { get; set; }
    }
}
