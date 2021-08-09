using System.Linq;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Services.Neighborhoods.Models;

namespace PerfectHomeToYou.Services.Neighborhoods
{
    public class NeighborhoodService : INeighborhoodService
    {
        private readonly PerfectHomeToYouDbContext context;

        public NeighborhoodService(PerfectHomeToYouDbContext context)
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
                .Select(n => new NeighborhoodServiceModel
                {
                    Id = n.Id,
                    Name = n.Name,
                    CityId = n.City.Id,
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
        
        public NeighborhoodDetailsServiceModel Details(int neighborhoodId)
            => this.context
                   .Neighborhoods
                   .Where(c => c.Id == neighborhoodId)
                   .Select(c => new NeighborhoodDetailsServiceModel
                   {
                       Id = c.Id,
                       Name = c.Name,
                       CityId = c.City.Id,
                       CityName = c.City.Name
                   })
                   .FirstOrDefault();

        public int Create(string name, int cityId)
        {
            var neighborhoodData = new Neighborhood
            {
                Name = name,
                CityId = cityId
            };

            this.context.Neighborhoods.Add(neighborhoodData);
            this.context.SaveChanges();

            return neighborhoodData.Id;
        }

        public bool Edit(int neighborhoodId, string name, int cityId)
        {
            var neighborhoodData = this.context.Neighborhoods.Find(neighborhoodId);

            if (neighborhoodData == null)
            {
                return false;
            }

            neighborhoodData.Name = name;
            neighborhoodData.CityId = cityId;

            this.context.SaveChanges();

            return true;
        }

        public bool NeighborhoodNameExist(string name)
            => this.context
                   .Neighborhoods
                   .Any(n => n.Name == name);

        public bool NeighborhoodExistInTheCity(string name)
            => this.context
                   .Neighborhoods
                   .Any(n => n.City.Neighborhoods.Any(n => n.Name == name));
    }
}
