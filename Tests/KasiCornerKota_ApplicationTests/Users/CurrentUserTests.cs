using FluentAssertions;
using KasiCornerKota_Domain.Constants;
using Xunit;


namespace KasiCornerKota_Application.Users.Tests
{
    public class CurrentUserTests
    {
        //Test method case expected result
        [Theory]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);
            //act
            var isInRole = currentUser.IsRole(roleName);
            //assert
            //Assert.True(isInRole);
            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);
            //act
            var isInRole = currentUser.IsRole(UserRoles.Owner);
            //assert
            //Assert.True(isInRole);
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsRole_WithNoMatchingCaseRole_ShouldReturnFalse()
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);
            //act
            var isInRole = currentUser.IsRole(UserRoles.Admin.ToLower());
            //assert
            //Assert.True(isInRole);
            isInRole.Should().BeFalse();
        }
    }
}