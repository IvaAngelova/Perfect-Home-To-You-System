using Microsoft.AspNetCore.Identity;
using MyTested.AspNetCore.Mvc;
using PerfectHomeToYou.Controllers;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Models.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PerfectHomeToYou.Test.Controllers
{
    public class CitiesControllerTest
    {
        [Fact]
        public void AddCityShouldReturnCorrectView()
           => MyController<CitiesController>
              .Instance()
              .WithUser(user => user.InRole("Administrator"))
              .Calling(c => c.Add())
              .ShouldReturn()
              .View();

        [Fact]
        public void AddCityShouldReturnBadRequestIfModelIsNotValid()
            => MyController<CitiesController>
              .Instance()
              .WithUser(user => user.InRole("Administrator"))
              .Calling(c => c.Add(With.Default<CityFormModel>()))
              .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(HttpMethod.Post))
              .AndAlso()
              .ShouldReturn()
              .BadRequest();

        [Fact]
        public void EditCityShouldReturnBadRequestIfModelIsNotValid()
            => MyController<CitiesController>
                    .Instance()
                    .WithUser(user => user.InRole("Administrator"))
                    .Calling(c => c.Add(With.Default<CityFormModel>()))
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                        .RestrictingForAuthorizedRequests()
                        .RestrictingForHttpMethod(HttpMethod.Post))
                    .AndAlso()
                    .ShouldReturn()
                    .BadRequest();
    }
}
