using MyTested.AspNetCore.Mvc;
using PerfectHomeToYou.Controllers;
using PerfectHomeToYou.Models.Neighborhoods;
using Xunit;

namespace PerfectHomeToYou.Test.Routing
{
    public class NeighborhoodsControllerTest
    {
        [Fact]
        public void GetAddShouldBeMapped()
               => MyRouting
                   .Configuration()
                   .ShouldMap("/Neighborhoods/Add")
                   .To<NeighborhoodsController>(c => c.Add());

        [Fact]
        public void PostAddShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Neighborhoods/Add")
                    .WithMethod(HttpMethod.Post))
                .To<NeighborhoodsController>(c => c.Add(With.Any<NeighborhoodFormModel>()));

        [Fact]
        public void DetailsShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap(request => request
                   .WithPath("/Neighborhoods/Details/1")
                   .WithMethod(HttpMethod.Post))
               .To<NeighborhoodsController>(c => c.Details(1));

        [Fact]
        public void AllShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Neighborhoods/All")
                .To<NeighborhoodsController>(c => c.All(With.Any<AllNeighborhoodsQueryModel>()));

        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Neighborhoods/Edit/1")
                .To<NeighborhoodsController>(c => c.Edit(1));

        [Fact]
        public void PostEditShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Neighborhoods/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<NeighborhoodsController>(c => c.Edit(1, With.Any<NeighborhoodFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Neighborhoods/Delete/1")
                .To<NeighborhoodsController>(c => c.Delete(1));
    }
}
