using System.Collections.Generic;

using PerfectHomeToYou.Models;
using PerfectHomeToYou.Models.Apartments;
using PerfectHomeToYou.Data.Models.Enumerations;
using PerfectHomeToYou.Services.Apartments.Models;

namespace PerfectHomeToYou.Services.Apartments
{
    public interface IApartmentService
    {
        ApartmentQueryServiceModel All(ApartmentsTypes apartmentType = ApartmentsTypes.OneBedroom,
           string searchTerm = null,
           ApartmentSorting sorting=ApartmentSorting.DateCreated,
           int currentPage = 1,
           int apartmentsPerPage = int.MaxValue,
           bool publicOnly = true);

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
                RentOrSale rentOrSale,
                bool isPublic);
        
        bool Delete(int apartmentId);

        public IEnumerable<ApartmentServiceModel> ByUser(string userId);

        bool IsByClient(int apartmentId, int clientId);

        bool CityExists(int cityId);

        bool NeighborhoodExists(int neighborhoodId);

        IEnumerable<CityViewModel> GetApartmentCities();

        IEnumerable<ApartmentNeighborhoodModel> GetApartmentNeighborhoods();

        public void ChangeVisibility(int apartmentId);
    }
}
