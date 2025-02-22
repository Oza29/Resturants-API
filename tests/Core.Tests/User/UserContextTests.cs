using Core.users;
using Core.Users;
using Domain.Constants;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace Core.Tests.User
{
    public class UserContextTests
    {
        [Fact]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUserObject()
        {
            //Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var dateOfBirth = new DateOnly(2000, 3, 22);
            var Claims = new List<Claim>
            {
                new (ClaimTypes.Email,"test@example.com"),
                new (ClaimTypes.NameIdentifier,"1"),
                new (ClaimTypes.Role,UserRoles.Admin),
                new (ClaimTypes.Role,UserRoles.User),
                new (nameof(CurrentUser.DateOfBirth),dateOfBirth.ToString("yyyy-MM-dd")),
                new (nameof(CurrentUser.Nationality),"Egyptian")
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(Claims,"Test"));
            httpContextAccessorMock.Setup(s => s.HttpContext).Returns(new DefaultHttpContext
            {
                User = user
            });
            

            UserContext userContext=new UserContext(httpContextAccessorMock.Object);

            //Act
          CurrentUser? result=  userContext.GetCurrentUser();
            //Assert
            result.Should().NotBeNull();
            result.Email.Should().Be("test@example.com");
            result.Id.Should().Be("1");
            result.DateOfBirth.Should().Be(dateOfBirth);
            result.Nationality.Should().Be("Egyptian");
            result.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        }

        [Fact]
        public void GetCurrentUser_WithNullUser_ShouldReturnException()
        {
            //Arrange
            var httpContextAccessor=new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(s => s.HttpContext).Returns( (DefaultHttpContext)null);
            var userContext = new UserContext(httpContextAccessor.Object);

            //Act
            Action action = () => userContext.GetCurrentUser();

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage("user context is not presented");
        }
    }
}
