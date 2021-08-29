using System.Linq;
using System.Collections.Generic;

using Xunit;

using Microsoft.EntityFrameworkCore;

using PerfectHomeToYou.Data;
using PerfectHomeToYou.Models;
using PerfectHomeToYou.Data.Models;
using PerfectHomeToYou.Models.Apartments;
using PerfectHomeToYou.Services.Apartments;
using PerfectHomeToYou.Data.Models.Enumerations;
using PerfectHomeToYou.Services.Apartments.Models;

namespace PerfectHomeToYou.Test.Services
{
    public class ApartmentServiceTest
    {
        private readonly PerfectHomeToYouDbContext context;
        private readonly IApartmentService apartmentService;
        public readonly DbContextOptions<PerfectHomeToYouDbContext> options;

        public ApartmentServiceTest()
        {
            options = new DbContextOptionsBuilder<PerfectHomeToYouDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb").Options;
            this.context = new PerfectHomeToYouDbContext(options);

            apartmentService = new ApartmentService(context);
        }

        //
        [Fact]
        public void ShouldReturnAllPublicAparptments()
        {
            //Arrange
            var user = new User()
            {
                Id = "6f8f30c5-85a5-4a76-844c-bd6488rgdcff",
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 682,
                FirstName = "Simona",
                LastName = "Angelova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 560,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 788,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);

            var firstApartment = new Apartment()
            {
                Id = 266,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            this.context.Apartments.Add(firstApartment);

            var secondApartment = new Apartment()
            {
                Id = 366,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = true
            };
            this.context.Apartments.Add(secondApartment);

            var thirdApartment = new Apartment()
            {
                Id = 326,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = true
            };
            this.context.Apartments.Add(thirdApartment);

            this.context.SaveChanges();

            //Act
            var expected = this.context.Apartments.Where(a => a.IsPublic).Count();
            var result = apartmentService.All();

            //Assert
            Assert.IsType<ApartmentQueryServiceModel>(result);
        }

        [Fact]
        public void LatestShoudReturnLastThreeApartments()
        {
            //Arrange
            var user = new User
            {
                Id = "6f8f31c5-85a5-4a76-844f-bd6488rgdcff"
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 365,
                FirstName = "Georgi",
                LastName = "Petrov",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 287,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 876,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);

            var firstApartment = new Apartment()
            {
                Id = 876,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = true
            };
            this.context.Apartments.Add(firstApartment);

            var secondApartment = new Apartment()
            {
                Id = 764,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = true
            };
            this.context.Apartments.Add(secondApartment);

            var thirdApartment = new Apartment()
            {
                Id = 265,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = true
            };
            this.context.Apartments.Add(thirdApartment);

            var fourthApartment = new Apartment()
            {
                Id = 825,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = true
            };
            this.context.Apartments.Add(fourthApartment);

            this.context.SaveChanges();

            //Act
            var result = apartmentService.Latest();

            //Assert
            Assert.Equal(3, result.Count());
            Assert.IsType<List<LatestApartmentServiceModel>>(result);
        }

        [Theory]
        [InlineData(555)]
        public void ShouldReturnApartmentDetails(int apartmentId)
        {
            //Arrange
            var user = new User()
            {
                Id = "6f8f31c5-85a5-4a76-844c-bd6488rgdcff",
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 882,
                FirstName = "Simona",
                LastName = "Angelova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 562,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 708,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);

            var apartment = new Apartment()
            {
                Id = 555,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            this.context.Apartments.Add(apartment);

            this.context.SaveChanges();

            //Act
            var expected = this.context.Apartments.FirstOrDefault(a => a.Id == apartmentId);
            var result = apartmentService.Details(apartmentId);

            //Assert
            Assert.IsType<ApartmentDetailsServiceModel>(result);
        }

        [Theory]
        [InlineData(ApartmentsTypes.ThreeBedroom, 888, 777, 5, "The apartment has the following functional layout - spacious living room",
            "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg", 555, RentOrSale.Rent, 982)]
        public void CreateShouldReturnNewApartmentId(ApartmentsTypes apartmentsTypes, int cityId, int neighborhoodId,
            int floor, string description, string imageUrl, decimal price, RentOrSale rentOrSale, int clientId)
        {
            //Arrange
            var user = new User()
            {
                Id = "6z8z30c5-85a5-4a76-844c-bd6488rgdcff",
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 982,
                FirstName = "Veronika",
                LastName = "Georgieva",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 888,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 777,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);
            
            this.context.SaveChanges();

            //Act
            var newApartmentId = apartmentService.Create(apartmentsTypes,cityId,neighborhoodId,floor,description,imageUrl,
                price,rentOrSale,clientId);

            //Assert
            Assert.IsType<int>(newApartmentId);
            Assert.NotEqual(0, newApartmentId);
        }

        [Theory]
        [InlineData(68, ApartmentsTypes.Penthouse, 123, 145, 5, "The apartment has the following functional layout - spacious living room",
            "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg", 567, RentOrSale.Rent, true)]
        public void ShouldReturnTrueIfEditingSuccessfulAndApartmentExist(int apartmentId, ApartmentsTypes apartmentsTypes, int cityId, int neighborhoodId,
            int floor, string description, string imageUrl, decimal price, RentOrSale rentOrSale, bool isPublic)
        {
            //Arrange
            var user = new User()
            {
                Id = "6f8f31c5-85a5-4a76-834c-bd6488rgdcff",
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 822,
                FirstName = "Simona",
                LastName = "Angelova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 123,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 145,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);

            var apartment = new Apartment()
            {
                Id = 68,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            this.context.Apartments.Add(apartment);

            this.context.SaveChanges();

            //Act
            var result = apartmentService.Edit(apartmentId, apartmentsTypes, cityId, neighborhoodId, floor,
                description, imageUrl, price, rentOrSale, isPublic);

            //Assert
            Assert.Equal(apartmentsTypes, apartment.ApartmentType);
            Assert.Equal(cityId, apartment.City.Id);
            Assert.Equal(neighborhoodId, apartment.Neighborhood.Id);
            Assert.Equal(floor, apartment.Floor);
            Assert.Equal(description, apartment.Description);
            Assert.Equal(imageUrl, apartment.ImageUrl);
            Assert.Equal(price, apartment.Price);
            Assert.Equal(rentOrSale, apartment.RentOrSale);
            Assert.Equal(isPublic, isPublic);
            Assert.True(result);
        }

        [Theory]
        [InlineData(717)]
        public void ShouldReturnFalseIfAndApartmentNotExist(int apartmentId)
        {
            //Arrange
            var user = new User()
            {
                Id = "6f8f30c5-46a5-4a76-844c-bd6488rgdcff",
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 621,
                FirstName = "Mihaela",
                LastName = "Angelova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 262,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 789,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);

            var apartment = new Apartment()
            {
                Id = 777,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            this.context.Apartments.Add(apartment);
            this.context.SaveChanges();

            //Act
            var result = apartmentService.Edit(apartmentId, ApartmentsTypes.Duplex, city.Id, neighborhood.Id,
                4, null, null, 456, RentOrSale.Rent, true);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(456)]
        public void ShouldRemoveApartmentIfExistAndReturnTrue(int apartmentId)
        {
            //Arrange
            var user = new User()
            {
                Id = "6f8f30c5-85a5-4a76-844c-bd6488d6dcff",
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 678,
                FirstName = "Simona",
                LastName = "Angelova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 546,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 87,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);

            var apartment = new Apartment()
            {
                Id = 456,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            this.context.Apartments.Add(apartment);
            this.context.SaveChanges();

            //Act
            var apartmentToRemove = context.Apartments.Find(apartmentId);
            var result = apartmentService.Delete(apartmentToRemove.Id);

            //Assert
            Assert.NotNull(apartmentToRemove);
            Assert.Equal(apartmentId, apartmentToRemove.Id);
            Assert.True(result);
        }

        [Theory]
        [InlineData(17)]
        public void ShouldReturnFalseIfApartmentNotExist(int apartmentId)
        {
            //Arrange
            var user = new User()
            {
                Id = "6s8h30c5-85a5-4a76-844c-bd6488d6dcff",
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 688,
                FirstName = "Simona",
                LastName = "Angelova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 464,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 878,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);

            var apartment = new Apartment()
            {
                Id = 469,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            this.context.Apartments.Add(apartment);
            this.context.SaveChanges();

            //Act
            var result = apartmentService.Delete(apartmentId);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(58, 45)]
        public void ShouldReturnTrueIfApartamentIsByClient(int apartmentId, int clientId)
        {
            //Arrange
            var user = new User()
            {
                Id = "4737ffdb-a6b7-45a6-a74c-2ee00ee4d9f9"
            };
            this.context.Users.Add(user);

            var client = new Client
            {
                Id = 45,
                FirstName = "Monika",
                LastName = "Petrova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var apartment = new Apartment()
            {
                Id = 58,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = 7,
                NeighborhoodId = 9,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            context.Apartments.Add(apartment);

            this.context.SaveChanges();

            //Act
            var result = apartmentService.IsByClient(apartmentId, clientId);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(69, 15)]
        public void ShouldReturnFalseIfApartamentIsNotByClient(int apartmentId, int clientId)
        {
            //Arrange
            var user = new User()
            {
                Id = "4737ffdb-a0b7-45a6-a74c-2ee00ee4d9f9"
            };
            this.context.Users.Add(user);

            var client = new Client
            {
                Id = 87,
                FirstName = "Monika",
                LastName = "Petrova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var apartment = new Apartment()
            {
                Id = 69,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = 7,
                NeighborhoodId = 9,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = 17,
                IsPublic = false
            };
            context.Apartments.Add(apartment);

            this.context.SaveChanges();

            //Act
            var result = apartmentService.IsByClient(apartmentId, clientId);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("d2777d63-83bf-4b79-99c3-7658128d7ae7")]
        public void ShouldReturnAllApartmentsByUser(string userId)
        {
            //Arrange
            var user = new User()
            {
                Id = "d2777d63-83bf-4b79-99c3-7658128d7ae7",
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 66,
                FirstName = "Iva",
                LastName = "Angelova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 876,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 456,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);

            var firstApartment = new Apartment()
            {
                Id = 15,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            this.context.Apartments.Add(firstApartment);

            var secondApartment = new Apartment()
            {
                Id = 20,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            this.context.Apartments.Add(secondApartment);

            this.context.SaveChanges();

            //Act
            var resultByUser = apartmentService.ByUser(userId);

            //Assert
            Assert.Equal(userId, user.Id);
            Assert.Equal(userId, client.UserId);
            Assert.Equal(client.Apartments.Count(), resultByUser.Count());
            Assert.IsType<List<ApartmentServiceModel>>(resultByUser);
        }

        [Fact]
        public void GetApartmentCitiesShouldReturnAllCities()
        {
            //Arrange
            var firstCity = new City
            {
                Id = 7,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(firstCity);

            var secondCity = new City
            {
                Id = 56,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(secondCity);

            this.context.SaveChanges();

            //Act
            var result = apartmentService.GetApartmentCities();

            //Assert
            Assert.Equal(this.context.Cities.Count(), result.Count());
            Assert.IsType<List<CityViewModel>>(result);
        }

        [Fact]
        public void GetApartmentNeighborhoodsReturnAllNeighborhoods()
        {
            //Arrange
            var city = new City()
            {
                Id = 39,
                Name = "Plovdiv",
                Postcode = "6000"
            };
            this.context.Cities.Add(city);

            var firstNeighborhood = new Neighborhood()
            {
                Id = 43,
                Name = "PlovdivTest",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(firstNeighborhood);

            var secondNeighborhood = new Neighborhood()
            {
                Id = 44,
                Name = "PlovdivTest2",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(secondNeighborhood);

            this.context.SaveChanges();

            //Act
            var result = apartmentService.GetApartmentNeighborhoods();

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(city.Neighborhoods);
            Assert.IsType<List<ApartmentNeighborhoodModel>>(result);
        }

        [Theory]
        [InlineData(20)]
        public void CityExistsShouldReturnTrueIfExist(int cityId)
        {
            //Arrange
            var city = new City
            {
                Id = 20,
                Name = "Sofia",
                Postcode = "1000"
            };

            this.context.Cities.Add(city);
            this.context.SaveChanges();

            //Act
            var result = apartmentService.CityExists(cityId);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(5)]
        public void CityExistsShouldReturnFalseIfNotExist(int cityId)
        {
            var city = new City
            {
                Id = 21,
                Name = "Sofia",
                Postcode = "1000"
            };

            this.context.Cities.Add(city);
            this.context.SaveChanges();

            //Act
            var result = apartmentService.CityExists(cityId);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        public void NeighborhoodExistsShouldReturnTrueIfExist(int neighborhoodId)
        {
            //Arrange
            var neighborhood = new Neighborhood
            {
                Id = 1,
                Name = "Vitosha",
                CityId = 7
            };

            this.context.Neighborhoods.Add(neighborhood);
            this.context.SaveChanges();

            //Act
            var result = apartmentService.NeighborhoodExists(neighborhoodId);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(5)]
        public void NeighborhoodExistsShouldReturnFalseIfNotExist(int neighborhoodId)
        {
            //Arrange
            var neighborhood = new Neighborhood
            {
                Id = 10,
                Name = "Vitosha",
                CityId = 7
            };

            this.context.Neighborhoods.Add(neighborhood);
            this.context.SaveChanges();

            //Act
            var result = apartmentService.NeighborhoodExists(neighborhoodId);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(77)]
        public void ChangeVisibilityIfApartmentIsNotPublic(int apartmentId)
        {
            //Arrange
            var user = new User()
            {
                Id = "d2647d63-86ff-4b79-99c3-7098128d7ae7",
            };
            this.context.Users.Add(user);

            var client = new Client()
            {
                Id = 19,
                FirstName = "Monika",
                LastName = "Angelova",
                PhoneNumber = "083 345 5432",
                UserId = user.Id
            };
            this.context.Clients.Add(client);

            var city = new City()
            {
                Id = 88,
                Name = "Sofia",
                Postcode = "1000"
            };
            this.context.Cities.Add(city);

            var neighborhood = new Neighborhood()
            {
                Id = 20,
                Name = "Vitosha",
                CityId = city.Id
            };
            this.context.Neighborhoods.Add(neighborhood);

            var apartment = new Apartment()
            {
                Id = 77,
                ApartmentType = ApartmentsTypes.OneBedroom,
                CityId = city.Id,
                NeighborhoodId = neighborhood.Id,
                Floor = 5,
                Description = "The apartment has the following functional layout - spacious living room",
                ImageUrl = "https://home2u.bg/wp-content/uploads/2020/07/a0a83e47-2da7-4d15-8bab-abec00eb4bd6.jpg",
                Price = 453,
                RentOrSale = RentOrSale.Rent,
                ClientId = client.Id,
                IsPublic = false
            };
            this.context.Apartments.Add(apartment);

            context.SaveChanges();


            //Act
            var changeApartment = context.Apartments.Find(apartmentId);
            this.apartmentService.ChangeVisibility(apartmentId);

            //Assert
            Assert.Equal(apartmentId, changeApartment.Id);
            Assert.NotNull(changeApartment);
            Assert.True(changeApartment.IsPublic);
        }
    }
}
