using PerfectHomeToYou.Data.Models.Enumerations;

namespace PerfectHomeToYou.Services.Apartments.Models
{
    public interface IApartmentModel
    {
        public ApartmentsTypes ApartmentType { get; }

        public string CityName { get; }

        public string NeighborhoodName { get; }
    }
}
