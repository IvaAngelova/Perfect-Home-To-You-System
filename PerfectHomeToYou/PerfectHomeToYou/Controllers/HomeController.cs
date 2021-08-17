using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using PerfectHomeToYou.Models;
using PerfectHomeToYou.Services.Apartments;
using PerfectHomeToYou.Services.Apartments.Models;

using static PerfectHomeToYou.WebConstants.Cache;

namespace PerfectHomeToYou.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly IApartmentService apartments;

        public HomeController(IMemoryCache cache, IApartmentService apartments)
        {
            this.cache = cache;
            this.apartments = apartments;
        }

        public IActionResult Index()
        {
            var latestApartments = this.cache
                .Get<List<LatestApartmentServiceModel>>(LatestApartmentsCacheKey);

            if (latestApartments == null)
            {
                latestApartments = this.apartments
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.cache.Set(LatestApartmentsCacheKey, latestApartments, cacheOptions);
            }

            return View(latestApartments);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
