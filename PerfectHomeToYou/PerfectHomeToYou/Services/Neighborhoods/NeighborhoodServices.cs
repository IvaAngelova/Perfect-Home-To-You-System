using System.Linq;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Data.Models;

namespace PerfectHomeToYou.Services.Neighborhoods
{
    public class NeighborhoodServices : INeighborhoodServices
    {
        private readonly PerfectHomeToYouDbContext context;

        public NeighborhoodServices(PerfectHomeToYouDbContext context)
            => this.context = context;

        public NeighborhoodQueryServiceModel All(string searchTerm, int currentPage, int neighborhoodsPerPage)
        {
            var neighborhoodQuery = this.context
                .Neighborhoods
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                neighborhoodQuery = neighborhoodQuery
                    .Where(n =>
                       n.Name.ToLower().Contains(searchTerm.ToLower())
                    || n.City.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalNeighborhoods = neighborhoodQuery.Count();

            var neighborhoods = neighborhoodQuery
                .Skip((currentPage - 1) * neighborhoodsPerPage)
                .Take(neighborhoodsPerPage)
                .Select(n => new NeighborhoodServicesModel
                {
                    Id = n.Id,
                    Name = n.Name,
                    CityName = n.City.Name
                })
                .ToList();

            return new NeighborhoodQueryServiceModel
            {
                TotalNeighborhoods = totalNeighborhoods,
                CurrentPage = currentPage,
                NeighborhoodsPerPage = neighborhoodsPerPage,
                Neighborhoods = neighborhoods
            };
        }
    }
}
