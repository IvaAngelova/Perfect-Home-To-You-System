using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models;
using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Services.Clients;
using PerfectHomeToYou.Models.Apartments;
using PerfectHomeToYou.Services.Apartments;

namespace PerfectHomeToYou.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IClientService clients;
        private readonly IApartmentServices apartments;
        private readonly PerfectHomeToYouDbContext context;

        public ApartmentsController(IClientService clients, IApartmentServices apartments,
            PerfectHomeToYouDbContext context)
        {
            this.clients = clients;
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
            var clientId = this.clients.IdByUser(this.User.Id());

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

            this.apartments.Create(apartment.ApartmentsTypes, apartment.CityId, apartment.NeighborhoodId,
                apartment.Floor, apartment.Description, apartment.ImageUrl, apartment.Price, apartment.RentOrSell, clientId);

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
                .Any(d => d.UserId == this.User.Id());

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
