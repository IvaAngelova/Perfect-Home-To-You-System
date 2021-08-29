using Microsoft.EntityFrameworkCore;
using PerfectHomeToYou.Data;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Data.Models.Enumerations;
using PerfectHomeToYou.Services.Apartments;
using PerfectHomeToYou.Services.Apartments.Models;
using PerfectHomeToYou.Test.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PerfectHomeToYou.Test.Services
{
    public class ApartmentServiceTesOldt
    {
        private readonly PerfectHomeToYouDbContext context;
        private readonly IApartmentService apartmentService;
        public readonly DbContextOptions<PerfectHomeToYouDbContext> options;

        public ApartmentServiceTesOldt()
        {
            options = new DbContextOptionsBuilder<PerfectHomeToYouDbContext>()
                .UseInMemoryDatabase(databaseName: "SettingsTestDb").Options;
            this.context = new PerfectHomeToYouDbContext(options);

            this.apartmentService = new ApartmentService(context);
        }

        //[Fact]
        //public void TestGetAllUsers()
        //{
        //    // Arrange
        //    var mock = new Mock<PerfectHomeToYouDbContext>();
        //    mock.Setup(x => x.Set<Apartment>())
        //        .Returns(new List<Apartment>
        //        {
        //            new Apartment
        //            {   Id = 15,
        //                ApartmentType = ApartmentsTypes.OneBedroom,
        //                CityId = 7,
        //                NeighborhoodId = 9,
        //                Floor = 5,
        //                Description = "The apartment has the following functional layout - spacious living room",
        //                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
        //                Price = 453,
        //                RentOrSale = RentOrSale.Rent,
        //                ClientId =1,
        //                IsPublic =false
        //            }
        //        });

        //    ApartmentService apartmentService = new ApartmentService(mock.Object);

        //    // Act
        //    var allApartments = apartmentService.All();

        //    // Assert
        //    Assert.Equal(1, allApartments.TotalApartments);
        //}
    }
}
