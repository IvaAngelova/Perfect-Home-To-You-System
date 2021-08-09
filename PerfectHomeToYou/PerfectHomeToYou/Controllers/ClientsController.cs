using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Models.Clients;
using PerfectHomeToYou.Services.Clients;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static PerfectHomeToYou.WebConstants;

namespace PerfectHomeToYou.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService clients;

        public ClientsController(IClientService clients) 
            => this.clients = clients;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeClientFormModel client)
        {
            var userId = this.User.Id();

            var userIsClient = this.clients
                .IsClient(userId);

            if (userIsClient)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(client);
            }

            this.clients.BecomeClient(client.FirstName, client.LastName, client.PhoneNumber, userId);

            TempData[GlobalMessageKey] = "Thank you for becamming a client!";

            return RedirectToAction("All", "Apartments");
        }
    }
}
