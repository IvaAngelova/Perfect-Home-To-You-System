using Xunit;
using MyTested.AspNetCore.Mvc;

using PerfectHomeToYou.Controllers;
using PerfectHomeToYou.Models.Cities;

namespace PerfectHomeToYou.Test.Routing
{
    public class CitiesControllerTest
    {
        [Fact]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cities/Add")
                .To<CitiesController>(c => c.Add());

        [Fact]
        public void PostAddShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Cities/Add")
                    .WithMethod(HttpMethod.Post))
                .To<CitiesController>(c => c.Add(With.Any<CityFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Cities/Details/1")
                    .WithMethod(HttpMethod.Post))
                .To<CitiesController>(c => c.Details(1));

        [Fact]
        public void AllShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cities/All")
                .To<CitiesController>(c => c.All(With.Any<AllCitiesQueryModel>()));


        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Cities/Edit/1")
                .To<CitiesController>(c => c.Edit(1));

        [Fact]
        public void PostEdithoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Cities/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<CitiesController>(c => c.Edit(1, With.Any<CityFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/Cities/Delete/1")
               .To<CitiesController>(c => c.Delete(1));
    }
}
