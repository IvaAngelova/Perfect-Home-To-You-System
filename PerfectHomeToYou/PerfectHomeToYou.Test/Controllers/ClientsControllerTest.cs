using Xunit;
using MyTested.AspNetCore.Mvc;

using PerfectHomeToYou.Controllers;
using PerfectHomeToYou.Models.Clients;

using static PerfectHomeToYou.WebConstants;

namespace PerfectHomeToYou.Test.Controllers
{
    public class ClientsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeAuthorizedUsersAndReturnView()
           => MyController<ClientsController>
               .Instance()
               .Calling(c => c.Become())
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View();

        [Theory]
        [InlineData("FirstName", "LastName","+35988 888 8888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string clientFirstName, string clientLastName, string phoneNumber)
            => MyController<ClientsController>
                .Instance(instance => instance
                    .WithUser())
                .Calling(c => c.Become(new BecomeClientFormModel
                {
                    FirstName = clientFirstName,
                    LastName = clientLastName,
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
