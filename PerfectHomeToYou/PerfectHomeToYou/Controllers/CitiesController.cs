using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models.Cities;
using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Services.Cities;
using PerfectHomeToYou.Services.Clients;

using static PerfectHomeToYou.WebConstants;

namespace PerfectHomeToYou.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityService cities;
        private readonly IClientService clients;

        public CitiesController(ICityService cities, IClientService clients)
        {
            this.cities = cities;
            this.clients = clients;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(CityFormModel city)
        {
            if (!this.User.IsInRole(AdministratorRoleName))
            {
                return BadRequest();
            }

            if (this.cities.CityExist(city.Name, city.Postcode))
            {
                this.ModelState.AddModelError(nameof(city.Name), "City already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            this.cities.Create(city.Name, city.Postcode);

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

        [Authorize]
        public IActionResult Edit(int id)
        {
            var city = this.cities.Details(id);
            
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new CityFormModel
            {
                Name = city.Name,
                Postcode = city.Postcode
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CityFormModel city)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.cities.Edit(id, city.Name, city.Postcode);

            return RedirectToAction(nameof(All));
        }
    }
}
