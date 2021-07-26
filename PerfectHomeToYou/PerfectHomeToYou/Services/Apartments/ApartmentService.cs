using System.Linq;
using System.Collections.Generic;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Models.Apartments;
using PerfectHomeToYou.Data.Models.Enumerations;

namespace PerfectHomeToYou.Services.Apartments
{
    public class ApartmentService : IApartmentService
    {
        private readonly PerfectHomeToYouDbContext context;

        public ApartmentService(PerfectHomeToYouDbContext context)
            => this.context = context;

        public ApartmentQueryServiceModel All(ApartmentsTypes apartmentType, string searchTerm,
            ApartmentSorting sorting, int currentPage, int apartmentsPerPage)
        {
            var apartmentQuery = this.context
                .Apartments
                .AsQueryable();

            if (apartmentType != 0)
            {
                apartmentQuery = apartmentQuery
                    .Where(a => a.ApartmentType.Equals(apartmentType));
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                apartmentQuery = apartmentQuery
                    .Where(a =>
                       a.City.Name.ToLower().Contains(searchTerm.ToLower())
                    || a.Neighborhood.Name.ToLower().Contains(searchTerm.ToLower())
                    || a.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            apartmentQuery = sorting switch
            {
                ApartmentSorting.Floor => apartmentQuery
                                        .OrderByDescending(a => a.Floor),
                ApartmentSorting.Rent => apartmentQuery
                                        .OrderByDescending(a => a.RentOrSell == RentOrSell.Rent),
                ApartmentSorting.Sell => apartmentQuery
                                        .OrderByDescending(a => a.RentOrSell == RentOrSell.Sell),
                ApartmentSorting.DateCreated or _ => apartmentQuery
                                        .OrderByDescending(a => a.Id)
            };

            var totalApartments = apartmentQuery.Count();

            var apartments = apartmentQuery
                .Skip((currentPage - 1) * apartmentsPerPage)
                .Take(apartmentsPerPage)
                .Select(a => new ApartmentServiceModel
                {
                    Id = a.Id,
                    ApartmentType = a.ApartmentType,
                    City = a.City.Name,
                    NeighborhoodId = a.Neighborhood.Id,
                    Floor = a.Floor,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Price = a.Price,
                    RentOrSell = a.RentOrSell
                })
                .ToList();

            return new ApartmentQueryServiceModel
            {
                TotalApartments = totalApartments,
                CurrentPage = currentPage,
                ApartmentsPerPage = apartmentsPerPage,
                Apartments = apartments
            };
        }

        public ApartmentDetailsServiceModel Details(int apartmentId)
            => this.context
                   .Apartments
                   .Where(a => a.Id == apartmentId)
                   .Select(a => new ApartmentDetailsServiceModel
                   {
                       Id = a.Id,
                       ApartmentType = a.ApartmentType,
                       City = a.City.Name,
                       NeighborhoodId = a.Neighborhood.Id,
                       Floor = a.Floor,
                       Description = a.Description,
                       ImageUrl = a.ImageUrl,
                       Price = a.Price,
                       RentOrSell = a.RentOrSell,
                       ClientId = a.ClientId,
                       ClientName = a.Client.LastName,
                       UserId = a.Client.UserId
                   })
                   .FirstOrDefault();

        public int Create(ApartmentsTypes apartmentsTypes, int cityId, int neighborhoodId,
            int floor, string description, string imageUrl, decimal price, RentOrSell rentOrSell, int clientId)
        {
            var apartmentData = new Apartment
            {
                ApartmentType = apartmentsTypes,
                CityId = cityId,
                NeighborhoodId = neighborhoodId,
                Floor = floor,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                RentOrSell = rentOrSell,
                ClientId = clientId
            };

            this.context.Apartments.Add(apartmentData);
            this.context.SaveChanges();

            return apartmentData.Id;
        }

        public bool Edit(int apartmentId, ApartmentsTypes apartmentsTypes, int cityId, int neighborhoodId,
            int floor, string description, string imageUrl, decimal price, RentOrSell rentOrSell)
        {
            var apartmentData = this.context.Apartments.Find(apartmentId);

            if (apartmentData == null)
            {
                return false;
            }

            apartmentData.ApartmentType = apartmentsTypes;
            apartmentData.ClientId = cityId;
            apartmentData.NeighborhoodId = neighborhoodId;
            apartmentData.Floor = floor;
            apartmentData.Description = description;
            apartmentData.ImageUrl = imageUrl;
            apartmentData.Price = price;
            apartmentData.RentOrSell = rentOrSell;

            this.context.SaveChanges();

            return true;
        }

        public bool IsByClient(int apartmentId, int clientId)
        => this.context
                .Apartments
                .Any(a => a.Id == apartmentId && a.ClientId == clientId);

        public IEnumerable<ApartmentServiceModel> ByUser(string userId)
             => GetApartments(this.context
                .Apartments
                .Where(c => c.Client.UserId == userId));

        private static IEnumerable<ApartmentServiceModel> GetApartments(IQueryable<Apartment> apartmentsQuery)
            => apartmentsQuery.Select(a => new ApartmentServiceModel
            {
                Id = a.Id,
                ApartmentType = a.ApartmentType,
                City = a.City.Name,
                NeighborhoodId = a.NeighborhoodId,
                Floor = a.Floor,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Price = a.Price,
                RentOrSell = a.RentOrSell
            })
            .ToList();

        public IEnumerable<CityViewModel> GetApartmentCities()
            => this.context
                    .Cities
                    .Select(c => new CityViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Postcode = c.Postcode
                    })
                    .ToList();

        public IEnumerable<ApartmentNeighborhoodModel> GetApartmentNeighborhoods()
            => this.context
                    .Neighborhoods
                    .Select(n => new ApartmentNeighborhoodModel
                    {
                        Id = n.Id,
                        Name = n.Name,
                        CityId = n.CityId
                    })
                    .ToList();

        public bool CityExists(int cityId)
            => this.context
            .Cities
            .Any(c => c.Id == cityId);

        public bool NeighborhoodExists(int neighborhoodId)
            => this.context
            .Neighborhoods
            .Any(c => c.Id == neighborhoodId);
    }
}
