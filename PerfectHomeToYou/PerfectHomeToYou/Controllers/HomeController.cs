using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models;
using PerfectHomeToYou.Models.Home;

namespace PerfectHomeToYou.Controllers
{
    public class HomeController : Controller
    {
        private readonly PerfectHomeToYouDbContext context;

        public HomeController(PerfectHomeToYouDbContext context) 
            => this.context = context;

        public IActionResult Index()
        {
            var apartments = this.context
                .Apartments
                .OrderByDescending(c => c.Id)
                .Select(c => new ApartmentIndexViewModel
                {
                    Id = c.Id,
                    City = c.City.Name,
                    Neighborhood = c.Neighborhood.Name,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    RentOrSell = c.RentOrSell.ToString()
                })
                .Take(3)
                .ToList();

            var totalApartments = apartments.Count();
            var totalUsers = this.context
                .Users
                .Count();

            return View(new IndexViewModel
            {
                TotalApartments = totalApartments,
                TotalUsers = totalUsers,
                Apartments = apartments
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
