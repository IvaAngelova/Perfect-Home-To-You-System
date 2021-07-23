using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Models.Apartments;
using PerfectHomeToYou.Services.Apartments;

namespace PerfectHomeToYou.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IApartmentServices apartments;
        private readonly PerfectHomeToYouDbContext context;

        public ApartmentsController(IApartmentServices apartments,
            PerfectHomeToYouDbContext context)
        {
            this.apartments = apartments;
            this.context = context;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsClient())
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            return View(new AddApartmentFormModel
            {
                Cities = this.GetApartmentCities(),
                Neighborhoods = this.GetApartmentNeighborhoods()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddApartmentFormModel apartment)
        {
            var clientId = this.context
                .Clients
                .Where(c => c.UserId == this.User.GetId())
                .Select(c => c.Id)
                .FirstOrDefault();

            if (clientId == 0)
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            if (!this.context.Cities.Any(c => c.Id == apartment.CityId))
            {
                this.ModelState.AddModelError(nameof(apartment.CityId), "City does not exist.");
            }

            if (!this.context.Neighborhoods.Any(c => c.Id == apartment.NeighborhoodId))
            {
                this.ModelState.AddModelError(nameof(apartment.NeighborhoodId), "Neighborhood does not exist.");
            }

            if (!ModelState.IsValid)
            {
                apartment.Cities = this.GetApartmentCities();
                apartment.Neighborhoods = this.GetApartmentNeighborhoods();

                return View(apartment);
            }

            var apartmentData = new Apartment
            {
                ApartmentType = apartment.ApartmentsTypes,
                CityId = apartment.CityId,
                NeighborhoodId = apartment.NeighborhoodId,
                Floor = apartment.Floor,
                Description = apartment.Description,
                ImageUrl = apartment.ImageUrl,
                Price = apartment.Price,
                RentOrSell = apartment.RentOrSell,
                ClientId = clientId
            };

            this.context.Apartments.Add(apartmentData);
            this.context.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllApartmentsQueryModel query)
        {
            var queryResult = this.apartments.All(
                query.ApartmentsTypes,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllApartmentsQueryModel.ApartmentsPerPage);

            query.TotalApartments = queryResult.TotalApartments;
            query.Apartments = queryResult.Apartments;

            return View(query);
        }

        private bool UserIsClient()
            => this.context
                .Clients
                .Any(d => d.UserId == this.User.GetId());

        private IEnumerable<CityViewModel> GetApartmentCities()
            => this.context
                    .Cities
                    .Select(c => new CityViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Postcode = c.Postcode
                    })
                    .ToList();

        private IEnumerable<ApartmentNeighborhoodViewModel> GetApartmentNeighborhoods()
            => this.context
                    .Neighborhoods
                    .Select(c => new ApartmentNeighborhoodViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();
    }
}
