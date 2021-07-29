using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Services.Cities;
using PerfectHomeToYou.Services.Clients;
using PerfectHomeToYou.Models.Neighborhoods;
using PerfectHomeToYou.Services.Neighborhoods;

using static PerfectHomeToYou.WebConstants;

namespace PerfectHomeToYou.Controllers
{
    public class NeighborhoodsController : Controller
    {
        private readonly IClientService clients;
        private readonly INeighborhoodService neighborhoods;
        private readonly ICityService cities;

        public NeighborhoodsController(IClientService clients, INeighborhoodService neighborhoods, 
            ICityService cities)
        {
            this.clients = clients;
            this.neighborhoods = neighborhoods;
            this.cities = cities;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            return View(new NeighborhoodFormModel
            {
                Cities = this.cities.GetCities()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(NeighborhoodFormModel neighborhood)
        {
            if (!User.IsInRole(AdministratorRoleName))
            {
                return BadRequest();
            }

            if (this.neighborhoods.NeighborhoodNameExist(neighborhood.Name))
            {
                this.ModelState.AddModelError(nameof(neighborhood.Name), "Neighborhood already exists");
            }

            if (this.neighborhoods.NeighborhoodExistInTheCity(neighborhood.Name))
            {
                this.ModelState.AddModelError(nameof(neighborhood), "Neighborhood already exists in the city");
            }

            if (!ModelState.IsValid)
            {
                neighborhood.Cities = this.cities.GetCities();

                return View(neighborhood);
            }

            this.neighborhoods.Create(neighborhood.Name, neighborhood.CityId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllNeighborhoodsQueryModel query)
        {
            var queryResult = this.neighborhoods.All(
                query.SearchTerm,
                query.CurrentPage,
                AllNeighborhoodsQueryModel.NeighborhoodsPerPage);

            query.TotalNeighborhoods = queryResult.TotalNeighborhoods;
            query.Neighborhoods = queryResult.Neighborhoods;

            return View(query);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var neighborhood = this.neighborhoods.Details(id);

            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new NeighborhoodFormModel
            {
                Name = neighborhood.Name,
                CityId = neighborhood.CityId,
                Cities = this.cities.GetCities()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, NeighborhoodFormModel neighborhood)
        {
            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.neighborhoods.Edit(id, neighborhood.Name, neighborhood.CityId);

            return RedirectToAction(nameof(All));
        }
    }
}
