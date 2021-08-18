using Xunit;
using MyTested.AspNetCore.Mvc;

using PerfectHomeToYou.Controllers;

namespace PerfectHomeToYou.Test.Roulting
{
    public class HomeControllerTest
    {
        [Fact]
        public void GetIndexRouteShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Index")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void GetErrorRouteShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
