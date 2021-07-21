using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Infrastructure;

namespace PerfectHomeToYou.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly PerfectHomeToYouDbContext context;

        public ApartmentsController(PerfectHomeToYouDbContext context)
        {
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

        private bool UserIsClient()
            => this.context
                .Clients
                .Any(d => d.UserId == this.User.GetId());
    }
}
