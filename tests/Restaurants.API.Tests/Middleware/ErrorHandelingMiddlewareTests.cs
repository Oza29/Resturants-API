using Domain.Entites;
using Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Resturants.API.Middleware;

namespace Restaurants.API.Tests.Middleware
{

    public class ErrorHandelingMiddlewareTests
    {
        private readonly Mock<ILogger<ErrorHandlingMiddleware>> _logger;
        private readonly DefaultHttpContext _context;
        private readonly ErrorHandlingMiddleware _middleware;
        public ErrorHandelingMiddlewareTests()
        {
            _logger=new Mock<ILogger<ErrorHandlingMiddleware>>();
            _context=new DefaultHttpContext();
            _middleware = new ErrorHandlingMiddleware(_logger.Object);
            
        }


        [Fact]
        public async Task InvokeAsync_WhenNoExceptionIsThrown_ShouldCallNextMiddleware()
        {
            //Arrange
            var next = new Mock<RequestDelegate>();
            //Act
            await _middleware.InvokeAsync(_context,next.Object);
            //Assert
            
            next.Verify(c=>c.Invoke(_context),Times.Once);

        }

        [Fact]
        public async Task InvokeAsync_WhenNotFoundExceptionIsThrown_ShouldSetStatusCode404()
        {
            //Arrange
            var exception = new NotFoundException(nameof(Restaurant),"1");
            //Act
            await _middleware.InvokeAsync(_context, _ => throw exception);
            //Arrange
            _context.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);

        }

        [Fact]
        public async Task InvokeAsync_WhenForbiddenExceptionIsThrown_ShouldSetStatusCode403()
        {
            //Arrange
            var exception = new ForbiddenException();
            //Act
            await _middleware.InvokeAsync(_context, _ => throw exception);

            //Assert
            _context.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
        }

        [Fact]
        public async Task InvokeAsync_WhenGenericExceptionIsThrown_ShouldSetStatusCode500()
        {
            //Arrange
            var exception = new Exception();
            //Act
            await _middleware.InvokeAsync(_context, _ => throw exception);
            //Assert
            _context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
