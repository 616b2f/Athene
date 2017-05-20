using System;
using Xunit;
using Athene.Abstractions;
using Athene.Abstractions.Models;

namespace Athene.AbstractionsTests
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _userService;
        public UserRepositoryTests()
        {
            _userService = new InMemoryUserRepository();
        }

        [Fact]
        public void AddUser()
        {
            var user = new TestUser
            {
               Id = "1",
               Surname = "Hans",
               Lastname = "Mustermann",
               Gender = Gender.Male,
               Birthsday = new DateTime(1970, 12, 1),
            };
            _userService.Add(user);
            var userTmp = _userService.FindByUserId("1");

            Assert.Equal(user.Id, userTmp.Id);
            Assert.Equal(user.Surname, userTmp.Surname);
            Assert.Equal(user.Lastname, userTmp.Lastname);
            Assert.Equal(user.Gender, userTmp.Gender);
            Assert.Equal(user.Birthsday, userTmp.Birthsday);
        }
    }
}
