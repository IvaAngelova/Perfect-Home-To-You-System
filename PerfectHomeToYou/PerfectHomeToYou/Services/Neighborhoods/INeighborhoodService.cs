using PerfectHomeToYou.Services.Neighborhoods;

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

        public bool NeighborhoodNameExist(string name);

        public bool NeighborhoodExistInTheCity(string name);
    }
}
