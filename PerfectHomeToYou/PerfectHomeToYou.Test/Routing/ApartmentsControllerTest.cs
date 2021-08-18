using Xunit;
using MyTested.AspNetCore.Mvc;

using PerfectHomeToYou.Controllers;
using PerfectHomeToYou.Models.Apartments;

namespace PerfectHomeToYou.Test.Roulting
{
    public class ApartmentsControllerTest
    {
        [Fact]
        public void GetAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Apartments/Add")
                .To<ApartmentsController>(c => c.Add());

        [Fact]
        public void PostAddShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Apartments/Add")
                    .WithMethod(HttpMethod.Post))
                .To<ApartmentsController>(c => c.Add(With.Any<ApartmentFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Apartments/Details/1/information")
                    .WithMethod(HttpMethod.Post))
                .To<ApartmentsController>(c => c.Details(1, "information"));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Apartments/Delete/1")
                .To<ApartmentsController>(c => c.Delete(1));

        [Fact]
        public void GetMineShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Apartments/Mine")
                .To<ApartmentsController>(c => c.Mine());

        [Fact]
        public void AllShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Apartments/All")
                .To<ApartmentsController>(c => c.All(With.Any<AllApartmentsQueryModel>()));

        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Apartments/Edit/1")
                .To<ApartmentsController>(c => c.Edit(1));

        [Fact]
        public void PostEditShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Apartments/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<ApartmentsController>(c => c.Edit(1, With.Any<ApartmentFormModel>()));
    }
}
