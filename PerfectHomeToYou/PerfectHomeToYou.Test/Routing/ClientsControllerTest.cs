using Xunit;
using MyTested.AspNetCore.Mvc;

using PerfectHomeToYou.Controllers;
using PerfectHomeToYou.Models.Clients;

namespace PerfectHomeToYou.Test.Roulting
{
    public class ClientsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Clients/Become")
                .To<ClientsController>(c => c.Become());

        [Fact]
        public void PostBecomeShoulBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Clients/Become")
                    .WithMethod(HttpMethod.Post))
                .To<ClientsController>(c => c.Become(With.Any<BecomeClientFormModel>()));
    }
}
