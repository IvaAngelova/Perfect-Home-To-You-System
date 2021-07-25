using PerfectHomeToYou.Models;
using PerfectHomeToYou.Data.Models.Enumerations;

namespace PerfectHomeToYou.Services.Apartments
{
    public interface IApartmentServices
    {
        ApartmentQueryServiceModel All(ApartmentsTypes apartmentType,
           string searchTerm,
           ApartmentSorting sorting,
           int currentPage,
           int apartmentsPerPage);

        int Create(ApartmentsTypes apartmentsTypes,
                int cityId,
                int neighborhoodId,
                int floor,
                string description,
                string imageUrl,
                decimal price,
                RentOrSell rentOrSell,
                int clientId);
    }
}
