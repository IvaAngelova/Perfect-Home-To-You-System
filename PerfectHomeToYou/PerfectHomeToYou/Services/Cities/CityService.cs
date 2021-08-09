using System.Linq;
using System.Collections.Generic;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Services.Cities.Models;

namespace PerfectHomeToYou.Services.Cities
{
    public class CityService : ICityService
    {
        private readonly PerfectHomeToYouDbContext context;

        public CityService(PerfectHomeToYouDbContext context) 
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
               .Select(c => new CityServiceModel
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

        public CityDetailsServiceModel Details(int cityId)
            => this.context
                   .Cities
                   .Where(c => c.Id == cityId)
                   .Select(c => new CityDetailsServiceModel
                   {
                       Id = c.Id,
                       Name = c.Name,
                       Postcode = c.Postcode,
                       Neighborhoods = c.Neighborhoods.ToList()
                   })
                   .FirstOrDefault();

        public int Create(string name, string postcode)
        {
            var cityData = new City
            {
                Name = name,
                Postcode = postcode
            };

            this.context.Cities.Add(cityData);
            this.context.SaveChanges();

            return cityData.Id;
        }
        
        public bool Edit(int cityId, string name, string postcode)
        {
            var cityData = this.context.Cities.Find(cityId);

            if (cityData == null)
            {
                return false;
            }

            cityData.Name = name;
            cityData.Postcode = postcode;

            this.context.SaveChanges();

            return true;
        }

        public IEnumerable<CityViewModel> GetCities()

            => this.context
                    .Cities
                    .Select(c => new CityViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Postcode = c.Postcode
                    })
                    .ToList();

        public bool CityExist(string name, string postcode)
            => this.context
                    .Cities
                    .Any(c => c.Name == name && c.Postcode == postcode);
    }
}
