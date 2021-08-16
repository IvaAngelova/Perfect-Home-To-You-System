using System.Collections.Generic;

using PerfectHomeToYou.Models;
using PerfectHomeToYou.Services.Cities.Models;

namespace PerfectHomeToYou.Services.Cities
{
    public interface ICityService
    {
        CityQueryServiceModel All(string searchTerm,
           int currentPage,
           int citiesPerPage);

        CityDetailsServiceModel Details(int cityId);

        int Create(string name, string postcode);
        
        bool Edit(int cityId,string name, string postcode);

        bool Delete(int cityId);

        public IEnumerable<CityViewModel> GetCities();

        public bool CityExist(string name, string postcode);
    }
}
