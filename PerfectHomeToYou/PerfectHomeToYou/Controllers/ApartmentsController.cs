using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Services.Clients;
using PerfectHomeToYou.Models.Apartments;
using PerfectHomeToYou.Services.Questions;
using PerfectHomeToYou.Services.Apartments;


using static PerfectHomeToYou.WebConstants;
using static PerfectHomeToYou.Areas.Admin.AdminConstants;

namespace PerfectHomeToYou.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IClientService clients;
        private readonly IQuestionService questions;
        private readonly IApartmentService apartments;

        public ApartmentsController(IClientService clients, IApartmentService apartments)
        {
            this.clients = clients;
            this.apartments = apartments;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.clients.IsClient(this.User.Id()) && !this.User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            return View(new ApartmentFormModel
            {
                Cities = this.apartments.GetApartmentCities(),
                Neighborhoods = this.apartments.GetApartmentNeighborhoods()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ApartmentFormModel apartment)
        {
            var clientId = this.clients.IdByUser(this.User.Id());

            if (clientId == 0)
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            if (!this.apartments.CityExists(apartment.CityId))
            {
                this.ModelState.AddModelError(nameof(apartment.CityId), "City does not exist.");
            }

            if (!this.apartments.NeighborhoodExists(apartment.NeighborhoodId))
            {
                this.ModelState.AddModelError(nameof(apartment.NeighborhoodId), "Neighborhood does not exist.");
            }

            if (!ModelState.IsValid)
            {
                apartment.Cities = this.apartments.GetApartmentCities();
                apartment.Neighborhoods = this.apartments.GetApartmentNeighborhoods();

                return View(apartment);
            }

            var apartmentId = this.apartments.Create(apartment.ApartmentType, apartment.CityId, apartment.NeighborhoodId,
                apartment.Floor, apartment.Description, apartment.ImageUrl, apartment.Price, apartment.RentOrSale, clientId);

            TempData[GlobalMessageKey] = "Your apartment was added and it is awaiting for approval!";

            return RedirectToAction(nameof(Details), new { id = apartmentId, information = apartment.GetInformation() });
        }

        public IActionResult Details(int id, string information)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var apartment = this.apartments.Details(id);

            if (apartment == null)
            {
                return NotFound();
            }

            if (information != apartment.GetInformation())
            {
                return BadRequest();
            }

            return View(apartment);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var apartment = this.apartments.Delete(id);

            if (!apartment)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myApartments = this.apartments
                .ByUser(this.User.Id());

            return View(myApartments);
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

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.clients.IsClient(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            var apartment = this.apartments.Details(id);

            if (apartment.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new ApartmentFormModel
            {
                ApartmentType = apartment.ApartmentType,
                CityId = apartment.CityId,
                Cities = this.apartments.GetApartmentCities(),
                NeighborhoodId = apartment.NeighborhoodId,
                Neighborhoods = this.apartments.GetApartmentNeighborhoods(),
                Floor = apartment.Floor,
                Description = apartment.Description,
                ImageUrl = apartment.ImageUrl,
                Price = apartment.Price,
                RentOrSale = apartment.RentOrSale
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, ApartmentFormModel apartment)
        {
            var clientId = this.clients.IdByUser(this.User.Id());

            if (clientId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            if (!this.apartments.IsByClient(id, clientId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.apartments.Edit(id, apartment.ApartmentType, apartment.CityId, apartment.NeighborhoodId,
                apartment.Floor, apartment.Description, apartment.ImageUrl, apartment.Price, apartment.RentOrSale, this.User.IsAdmin());

            TempData[GlobalMessageKey] = $"Your car was edited{(this.User.IsAdmin() ? string.Empty : "and it is awaiting for approval")!}";

            return RedirectToAction(nameof(Details), new { id, information = apartment.GetInformation() });
        }
    }
}
