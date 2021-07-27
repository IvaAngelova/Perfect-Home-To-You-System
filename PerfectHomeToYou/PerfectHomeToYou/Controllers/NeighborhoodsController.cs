using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Models.Neighborhoods;
using PerfectHomeToYou.Services.Neighborhoods;

using static PerfectHomeToYou.WebConstants;

namespace PerfectHomeToYou.Controllers
{
    public class NeighborhoodsController : Controller
    {
        private readonly INeighborhoodServices neighborhoods;
        private readonly PerfectHomeToYouDbContext context;

        public NeighborhoodsController(INeighborhoodServices neighborhoods, PerfectHomeToYouDbContext context)
        {
            this.neighborhoods = neighborhoods;
            this.context = context;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsClient() && !User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            return View(new AddNeighborhoodFormModel
            {
                Cities = this.GetCities()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddNeighborhoodFormModel neighborhood)
        {
            var clientId = this.context
                .Clients
                .Where(c => c.UserId == this.User.Id())
                .Select(c => c.Id)
                .FirstOrDefault();

            if (clientId == 0 && !User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction(nameof(ClientsController.Become), "Clients");
            }

            if (this.context.Neighborhoods.Any(n => n.Name == neighborhood.Name))
            {
                this.ModelState.AddModelError(nameof(neighborhood.Name), "Neighborhood already exists");
            }

            if (this.context.Neighborhoods.Any(n => n.City.Neighborhoods.Any(n=>n.Name == neighborhood.Name)))
            {
                this.ModelState.AddModelError(nameof(neighborhood), "Neighborhood already exists in the city");
            }

            if (!ModelState.IsValid)
            {
                neighborhood.Cities = this.GetCities();

                return View(neighborhood);
            }

            var neighborhoodData = new Neighborhood
            {
                Name = neighborhood.Name,
                CityId = neighborhood.CityId
            };

            this.context.Neighborhoods.Add(neighborhoodData);
            this.context.SaveChanges();

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

        private IEnumerable<CityViewModel> GetCities()
            => this.context
                    .Cities
                    .Select(c => new CityViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Postcode = c.Postcode
                    })
                    .ToList();

        private bool UserIsClient()
            => this.context
                .Clients
                .Any(d => d.UserId == this.User.Id());
    }
}
