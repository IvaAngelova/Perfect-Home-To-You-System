using System.Linq;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Infrastructure;
using PerfectHomeToYou.Models.Clients;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PerfectHomeToYou.Controllers
{
    public class ClientsController : Controller
    {
        private readonly PerfectHomeToYouDbContext context;

        public ClientsController(PerfectHomeToYouDbContext context) 
            => this.context = context;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeClientFormModel client)
        {
            var userId = this.User.Id();

            var userIsAlreadyClient = this.context
                .Clients
                .Any(d => d.UserId == userId);

            if (userIsAlreadyClient)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(client);
            }

            var clientData = new Client
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                UserId = userId
            };

            this.context.Clients.Add(clientData);
            this.context.SaveChanges();

            return RedirectToAction("All", "Apartments");
        }
    }
}
