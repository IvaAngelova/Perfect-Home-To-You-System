using System.Collections.Generic;

using PerfectHomeToYou.Models;
using PerfectHomeToYou.Models.Apartments;
using PerfectHomeToYou.Data.Models.Enumerations;
using PerfectHomeToYou.Services.Apartments.Models;

namespace PerfectHomeToYou.Services.Apartments
{
    public interface IApartmentService
    {
        ApartmentQueryServiceModel All(ApartmentsTypes apartmentType,
           string searchTerm,
           ApartmentSorting sorting,
           int currentPage,
           int apartmentsPerPage);

        IEnumerable<LatestApartmentServiceModel> Latest();

        ApartmentDetailsServiceModel Details(int apartmentId);

        int Create(ApartmentsTypes apartmentsTypes,
                int cityId,
                int neighborhoodId,
                int floor,
                string description,
                string imageUrl,
                decimal price,
                RentOrSale rentOrSale,
                int clientId);

        bool Edit(int apartmentId,
                ApartmentsTypes apartmentsTypes,
                int cityId,
                int neighborhoodId,
                int floor,
                string description,
                string imageUrl,
                decimal price,
                RentOrSale rentOrSale);
        
        bool Delete(int apartmentId);

        public IEnumerable<ApartmentServiceModel> ByUser(string userId);

        bool IsByClient(int apartmentId, int clientId);

        bool CityExists(int cityId);

        bool NeighborhoodExists(int neighborhoodId);

        IEnumerable<CityViewModel> GetApartmentCities();

        IEnumerable<ApartmentNeighborhoodModel> GetApartmentNeighborhoods();
    }
}
