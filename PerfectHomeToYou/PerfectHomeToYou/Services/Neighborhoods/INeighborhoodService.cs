using PerfectHomeToYou.Services.Neighborhoods.Models;

namespace PerfectHomeToYou.Services.Neighborhoods
{
    public interface INeighborhoodService
    {
        NeighborhoodQueryServiceModel All(string searchTerm,
           int currentPage,
           int neighborhoodsPerPage);

        NeighborhoodDetailsServiceModel Details(int neighborhoodId);

        int Create(string name, int cityId);

        bool Edit(int neighborhoodId, string name, int cityId);

        bool Delete(int neighborhoodId);

        public bool NeighborhoodNameExist(string name);

        public bool NeighborhoodExistInTheCity(string name);
    }
}
