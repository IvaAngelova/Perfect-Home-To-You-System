using System;
using System.Collections.Generic;

using Xunit;
using FluentAssertions;
using MyTested.AspNetCore.Mvc;

using PerfectHomeToYou.Controllers;
using PerfectHomeToYou.Services.Apartments.Models;

using static PerfectHomeToYou.WebConstants.Cache;
using static PerfectHomeToYou.Test.Data.Apartments;

namespace PerfectHomeToYou.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexActionShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                .Instance(instance => instance
                    .WithData(TenPublicApartments()))
                .Calling(c => c.Index())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(LatestApartmentsCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(10))
                        .WithValueOfType<List<LatestApartmentServiceModel>>()))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<LatestApartmentServiceModel>>());

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
