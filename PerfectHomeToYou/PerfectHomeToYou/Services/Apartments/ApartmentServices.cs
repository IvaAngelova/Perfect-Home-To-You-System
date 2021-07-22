using System.Linq;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models;
using PerfectHomeToYou.Data.Models.Enumerations;
using System.Collections.Generic;

namespace PerfectHomeToYou.Services.Apartments
{
    public class ApartmentServices : IApartmentServices
    {
        private readonly PerfectHomeToYouDbContext context;

        public ApartmentServices(PerfectHomeToYouDbContext context)
            => this.context = context;

        public ApartmentQueryServiceModel All(ApartmentsTypes apartmentType, string searchTerm,
            ApartmentSorting sorting, int currentPage, int apartmentsPerPage)
        {
            var apartmentQuery = this.context
                .Apartments
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(apartmentType.ToString()))
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
                                        .OrderByDescending(a => a.RentOrSell.Equals("Rent")),
                ApartmentSorting.Sell => apartmentQuery
                                        .OrderByDescending(a => a.RentOrSell.Equals("Sell")),
                ApartmentSorting.DateCreated or _ => apartmentQuery
                                        .OrderByDescending(a => a.Id)
            };

            var totalApartments = apartmentQuery.Count();

            var apartments = apartmentQuery
                .Skip((currentPage - 1) * apartmentsPerPage)
                .Take(apartmentsPerPage)
                .Select(a => new ApartmentServicesModel
                {
                    Id = a.Id,
                    ApartmentType = a.ApartmentType.ToString(),
                    City = a.City.Name,
                    Neighborhood = a.Neighborhood.Name,
                    Floor = a.Floor,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Price = a.Price,
                    RentOrSell = a.RentOrSell.ToString()
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
    }
}
