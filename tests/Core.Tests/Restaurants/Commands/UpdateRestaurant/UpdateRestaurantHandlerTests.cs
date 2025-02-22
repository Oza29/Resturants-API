using AutoMapper;
using Castle.Core.Logging;
using Core.RepositoryContracts;
using Core.Restaurants.Commands.CreateRestaurant;
using Core.Restaurants.Commands.UpdateRestaurant;
using Domain.Constants;
using Domain.Entites;
using Domain.Exceptions;
using Domain.ServiceContract;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.AccessControl;

namespace Core.Tests.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantHandlerTests
    {

        private readonly Mock< ILogger<UpdateRestaurantHandler>> _loggerMock;
        private readonly Mock< IMapper> _mapperMock;
        private readonly Mock< IRestaurantRepository> _repositoryMock;
        private readonly Mock< IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;
        private readonly UpdateRestaurantHandler _handler;

        public UpdateRestaurantHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateRestaurantHandler>>();
            _mapperMock= new Mock<IMapper>();
            _repositoryMock = new Mock<IRestaurantRepository>();
            _restaurantAuthorizationServiceMock= new Mock <IRestaurantAuthorizationService>();
            _handler = new UpdateRestaurantHandler(_loggerMock.Object, _mapperMock.Object, _repositoryMock.Object
                , _restaurantAuthorizationServiceMock.Object);
        }

        [Fact]
        public async Task Handler_WithValidCommand_ShouldUpdateRestaurantSuccessfully()
        {
            //Arrange
            UpdateRestaurantCommand command = new()
            {
               ID =1,
                Name="test",
                Description="this is a description",
                HasDelivery=true
            };
            Restaurant ExistingRestaurant = new()
            {
                ID = command.ID,
                Name = "Existing Test",
                Description = "Existing Description"
            };

            _repositoryMock.Setup(c => c.GetRestaurantAsync(command.ID)).ReturnsAsync(ExistingRestaurant);
            _restaurantAuthorizationServiceMock.Setup(c=>c.Authorize(ExistingRestaurant,ResourceOperation.Update)).Returns(true);

            //Act
            await _handler.Handle(command, CancellationToken.None);


            //Assert
            _repositoryMock.Verify(c => c.SaveChanges(), Times.Once);
            _mapperMock.Verify(c => c.Map(command, ExistingRestaurant),Times.Once);
        }



        [Fact]
        public async Task Handler_WithNullRestaurant_ShouldThrowNotFoundException()
        {
            //Arrange
            UpdateRestaurantCommand command = new()
            {
                ID = 1, 
            };

            _repositoryMock.Setup(c => c.GetRestaurantAsync(command.ID)).ReturnsAsync((Restaurant?)null);

            //Act
            Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

            //Assert
            await action.Should().ThrowAsync<NotFoundException>().WithMessage($"{nameof(Restaurant)} with id: {command.ID} dosen't exsist");
        }




        [Fact]
        public async Task Handler_WithInvalidAuthorization_ShouldThrowForbiddenException()
        {
            //Arrange
            UpdateRestaurantCommand command = new()
            {
                ID = 1,

            };
            Restaurant ExistingRestaurant = new()
            {
                ID = command.ID,
            };
            _repositoryMock.Setup(c => c.GetRestaurantAsync(command.ID)).ReturnsAsync(ExistingRestaurant);
            _restaurantAuthorizationServiceMock.Setup(c => c.Authorize(ExistingRestaurant, ResourceOperation.Update)).Returns(false);

            //Act
            Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);


            //Assert
            await action.Should().ThrowAsync<ForbiddenException>();
        }
    }
}
