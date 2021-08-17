using System.Linq;

using Microsoft.AspNetCore.Mvc;

using PerfectHomeToYou.Services.Apartments;

namespace PerfectHomeToYou.Areas.Admin.Controllers
{
    public class ApartmentsController : AdminController
    {
        private readonly IApartmentService apartments;

        public ApartmentsController(IApartmentService apartments) 
            => this.apartments = apartments;

        public IActionResult All()
        {
            var apartments = this.apartments
                .All(publicOnly: false)
                .Apartments
                .ToList();

            apartments
                .OrderBy(a => a.IsPublic == false);

            return View(apartments);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.apartments.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
