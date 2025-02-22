using Core.Users;
using Domain.Constants;
using FluentAssertions;
using System.Reflection;

namespace Core.Tests.User
{
    public class CurrentUserTests
    {
        [Fact]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue()
        {
            //arrange
            CurrentUser user = new CurrentUser("1","user@example.com",[UserRoles.Admin,UserRoles.User],null,null);

            //act
          bool result=  user.IsInRole(UserRoles.Admin);

            //assert
            result.Should().BeTrue();

        }
        [Fact]
        public void IsInRole_WithWrongRoleCase_ShouldReturnFalse()
        {
            //arrange
            CurrentUser user = new CurrentUser("1", "user@example.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            bool result = user.IsInRole(UserRoles.Admin.ToLower());

            //assert
            result.Should().BeFalse();

        }
    }
}
