using System.Linq;
using System.Collections.Generic;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Models.Apartments;
using PerfectHomeToYou.Data.Models.Enumerations;
using PerfectHomeToYou.Services.Apartments.Models;

namespace PerfectHomeToYou.Services.Apartments
{
    public class ApartmentService : IApartmentService
    {
        private readonly PerfectHomeToYouDbContext context;

        public ApartmentService(PerfectHomeToYouDbContext context)
            => this.context = context;

        public ApartmentQueryServiceModel All(ApartmentsTypes apartmentType = ApartmentsTypes.OneBedroom, string searchTerm = null,
           ApartmentSorting sorting = ApartmentSorting.DateCreated, int currentPage = 1,
           int apartmentsPerPage = int.MaxValue, bool publicOnly = true)
        {
            var apartmentQuery = this.context
                .Apartments
                .Where(a => !publicOnly || a.IsPublic);

            if (apartmentType != 0 && sorting.Equals(null))
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
                                        .OrderByDescending(a => a.RentOrSale == RentOrSale.Rent),
                ApartmentSorting.Sale => apartmentQuery
                                        .OrderByDescending(a => a.RentOrSale == RentOrSale.Sale),
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
                    CityName = a.City.Name,
                    NeighborhoodId = a.Neighborhood.Id,
                    Floor = a.Floor,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Price = a.Price,
                    RentOrSale = a.RentOrSale,
                    IsPublic = a.IsPublic
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

        public IEnumerable<LatestApartmentServiceModel> Latest()
            => this.context
                .Apartments
                .Where(a => a.IsPublic)
                .OrderByDescending(c => c.Id)
                .Select(c => new LatestApartmentServiceModel
                {
                    Id = c.Id,
                    ApartmentType = c.ApartmentType,
                    CityName = c.City.Name,
                    NeighborhoodName = c.Neighborhood.Name,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    RentOrSale = c.RentOrSale.ToString()
                })
                .Take(3)
                .ToList();

        public ApartmentDetailsServiceModel Details(int apartmentId)

            => this.context
                   .Apartments
                   .Where(a => a.Id == apartmentId)
                   .Select(a => new ApartmentDetailsServiceModel
                   {
                       Id = a.Id,
                       ApartmentType = a.ApartmentType,
                       CityId = a.City.Id,
                       CityName = a.City.Name,
                       NeighborhoodId = a.Neighborhood.Id,
                       NeighborhoodName = a.Neighborhood.Name,
                       Floor = a.Floor,
                       Description = a.Description,
                       ImageUrl = a.ImageUrl,
                       Price = a.Price,
                       RentOrSale = a.RentOrSale,
                       ClientId = a.ClientId,
                       ClientName = a.Client.LastName,
                       UserId = a.Client.UserId
                   })
                   .FirstOrDefault();

        public int Create(ApartmentsTypes apartmentsTypes, int cityId, int neighborhoodId,
            int floor, string description, string imageUrl, decimal price, RentOrSale rentOrSale, int clientId)
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
                RentOrSale = rentOrSale,
                ClientId = clientId,
                IsPublic = false
            };

            this.context.Apartments.Add(apartmentData);
            this.context.SaveChanges();

            return apartmentData.Id;
        }

        public bool Edit(int apartmentId, ApartmentsTypes apartmentsTypes, int cityId, int neighborhoodId,
            int floor, string description, string imageUrl, decimal price, RentOrSale rentOrSale, bool isPublic)
        {
            var apartmentData = this.context.Apartments.Find(apartmentId);

            if (apartmentData == null)
            {
                return false;
            }

            apartmentData.ApartmentType = apartmentsTypes;
            apartmentData.CityId = cityId;
            apartmentData.NeighborhoodId = neighborhoodId;
            apartmentData.Floor = floor;
            apartmentData.Description = description;
            apartmentData.ImageUrl = imageUrl;
            apartmentData.Price = price;
            apartmentData.RentOrSale = rentOrSale;
            apartmentData.IsPublic = isPublic;

            this.context.SaveChanges();

            return true;
        }

        public bool Delete(int apartmentId)
        {
            var apartmentData = this.context.Apartments.Find(apartmentId);

            if (apartmentData == null)
            {
                return false;
            }

            this.context.Apartments.Remove(apartmentData);
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
                CityName = a.City.Name,
                NeighborhoodId = a.NeighborhoodId,
                Floor = a.Floor,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Price = a.Price,
                RentOrSale = a.RentOrSale
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
                        CityId = n.CityId,
                        CityName = n.City.Name
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

        public void ChangeVisibility(int apartmentId)
        {
            var apartment = this.context.Apartments.Find(apartmentId);

            apartment.IsPublic = !apartment.IsPublic;

            this.context.SaveChanges();
        }
    }
}
