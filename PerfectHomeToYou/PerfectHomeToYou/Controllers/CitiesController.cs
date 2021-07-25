using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Models.Cities;
using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Services.Cities;

namespace PerfectHomeToYou.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityServices cities;
        private readonly PerfectHomeToYouDbContext context;

        public CitiesController(ICityServices cities, PerfectHomeToYouDbContext context)
        {
            this.cities = cities;
            this.context = context;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsClient())
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddCityFormModel city)
        {
            var clientId = this.context
                .Clients
                .Where(c => c.UserId == this.User.Id())
                .Select(c => c.Id)
                .FirstOrDefault();

            if (clientId == 0)
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            if (this.context.Cities.Any(c => c.Name == city.Name && c.Postcode == city.Postcode))
            {
                this.ModelState.AddModelError(nameof(city.Name), "City already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cityData = new City
            {
                Name = city.Name,
                Postcode = city.Postcode
            };

            this.context.Cities.Add(cityData);
            this.context.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllCitiesQueryModel query)
        {
            var queryResult = this.cities.All(
                query.SearchTerm,
                query.CurrentPage,
                AllCitiesQueryModel.CitiesPerPage);

            query.TotalCities = queryResult.TotalCities;
            query.Cities = queryResult.Cities;

            return View(query);
        }

        private bool UserIsClient()
        => this.context
            .Clients
            .Any(d => d.UserId == this.User.Id());
    }
}
