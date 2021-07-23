using System.Linq;

using PerfectHomeToYou.Data;

namespace PerfectHomeToYou.Services.Cities
{
    public class CityServices : ICityServices
    {
        private readonly PerfectHomeToYouDbContext context;

        public CityServices(PerfectHomeToYouDbContext context) 
            => this.context = context;

        public CityQueryServiceModel All(string searchTerm, int currentPage, int citiesPerPage)
        {
            var cityQuery = this.context
                .Cities
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                cityQuery = cityQuery
                    .Where(c =>
                       c.Name.ToLower().Contains(searchTerm.ToLower())
                    || c.Postcode.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalCities = cityQuery.Count();

            var cities = cityQuery
               .Skip((currentPage - 1) * citiesPerPage)
               .Take(citiesPerPage)
               .Select(c => new CityServicesModel
               {
                   Id = c.Id,
                   Name = c.Name,
                   Postcode = c.Postcode
               })
               .ToList();

            return new CityQueryServiceModel
            {
                CurrentPage = currentPage,
                CitiesPerPage = citiesPerPage,
                TotalCities = totalCities,
                Cities = cities
            };
        }
    }
}
