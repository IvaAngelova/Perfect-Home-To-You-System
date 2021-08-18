using Xunit;
using MyTested.AspNetCore.Mvc;

using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Controllers;
using PerfectHomeToYou.Models.Apartments;

namespace PerfectHomeToYou.Test.Controllers
{
    public class ApartmentsControllerTest
    {
        [Fact]
        public void AddApartmetShouldReturnCorrectViewWithCitiesAndNeighborhoods()
            => MyController<ApartmentsController>
               .Instance(instance => instance
                    .WithUser()
               .WithData(new Client()
               {
                   Id = 1,
                   FirstName = "TestFirst",
                   LastName = "TestLast",
                   UserId = "test1"
               })
               .WithUser(user => user
                    .WithIdentifier("test1")
                    .WithUsername("TestFirst")
                    .InRole("Client")))
               .Calling(c => c.Add())
               .ShouldHave()
               .ActionAttributes(attributes => attributes
                   .RestrictingForAuthorizedRequests())
               .AndAlso()
               .ShouldReturn()
               .View(view => view.WithModelOfType<ApartmentFormModel>());

        [Fact]
        public void AddApartmentsShouldAddApartmentAndRedirectToDetails()
            => MyController<ApartmentsController>
                    .Instance(instance => instance
                    .WithUser()
                       .WithData(new Client()
                       {
                           Id = 1,
                           FirstName = "TestFirst",
                           LastName = "TestLast",
                           UserId = "test1"
                       })
                       .WithUser(user => user
                       .WithIdentifier("test1")
                       .WithUsername("TestFirst")
                       .InRole("Client")))
                    .Calling(a => a.Add(With.Default<ApartmentFormModel>()))
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(HttpMethod.Post))
                    .AndAlso()
                    .ShouldReturn()
                    .View();

        [Fact]
        public void MineApartmentsShouldReturnView()
            => MyController<ApartmentsController>
                   .Instance(instance => instance
                        .WithUser()
                   .WithData(new Client()
                   {
                       Id = 1,
                       FirstName = "TestFirst",
                       LastName = "TestLast",
                       UserId = "test1"
                   })
                   .WithUser(user => user
                        .WithIdentifier("test1")
                        .WithUsername("TestFirst")
                        .InRole("Client")))
                   .Calling(a => a.Mine())
                   .ShouldReturn()
                   .View();
    }
}
