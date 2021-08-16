using System;

using PerfectHomeToYou.Data.Models.Enumerations;

namespace PerfectHomeToYou.Services.Questions.Models
{
    public class QuestionDetailsServiceModel
    {
        public int Id { get; init; }

        public ApartmentsTypes ApartmentType { get; set; }

        public string CityName { get; init; }

        public string NeighborhoodName { get; init; }

        public decimal Price { get; set; }

        public RentOrSale RentOrSale { get; set; }

        public string Message { get; init; }

        public DateTime DateOfCreation { get; set; }
    }
}
