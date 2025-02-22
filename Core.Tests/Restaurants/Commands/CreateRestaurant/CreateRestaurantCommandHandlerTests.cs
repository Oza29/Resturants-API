using AutoMapper;
using Castle.Core.Logging;
using Core.RepositoryContracts;
using Core.Restaurants.Commands.CreateRestaurant;
using Core.users;
using Core.Users;
using Domain.Entites;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Core.Tests.Restaurants.Commands.CreateRestaurant
{
    
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact]
        public async Task Handler_WithValidCommand_ShouldReturnTheCreatedRestaurantId()
        {
            //Arrange 
            var mapperMock = new Mock<IMapper>();
            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();
            mapperMock.Setup(c=>c.Map<Restaurant>(command)).Returns(restaurant);
            var LoggerMock=new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var RepositoryMock = new Mock<IRestaurantRepository>();
            RepositoryMock.Setup(c => c.AddRestaurantAsync(It.IsAny<Restaurant>())).ReturnsAsync(1);
            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("OwnerId", "test@example", []);
            userContextMock.Setup(c => c.GetCurrentUser()).Returns(currentUser);
            var commandHandler = new CreateRestaurantCommandHandler(RepositoryMock.Object, LoggerMock.Object, mapperMock.Object, userContextMock.Object);

            //Act
            var result = await commandHandler.Handle(command,CancellationToken.None);

            //Assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("OwnerId");
            RepositoryMock.Verify(r => r.AddRestaurantAsync(restaurant), Times.Once);

        }
    }
}
