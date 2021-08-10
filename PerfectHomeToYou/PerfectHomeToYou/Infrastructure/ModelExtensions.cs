using PerfectHomeToYou.Services.Apartments.Models;

namespace PerfectHomeToYou.Infrastructure
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IApartmentModel apartment)
           => apartment.ApartmentType + "-" + apartment.CityName;
    }
}
