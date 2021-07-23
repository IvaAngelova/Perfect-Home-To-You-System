using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Models.Neighborhoods;

namespace PerfectHomeToYou.Controllers
{
    public class NeighborhoodsController : Controller
    {
        private readonly PerfectHomeToYouDbContext context;

        public NeighborhoodsController(PerfectHomeToYouDbContext context) 
            => this.context = context;

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
        public IActionResult Add(AddNeighborhoodFormModel neighborhood)
        {
            return View();
        }

        private bool UserIsClient()
            => this.context
                .Clients
                .Any(d => d.UserId == this.User.GetId());
    }
}
