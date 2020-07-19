using Data.Access;
using Data.Entities;
using NUnit.Framework;
using Supper.Controllers;

namespace ManualTest
{
    public class UserAccessTests
    {
        UserAccess dao = new UserAccess();
        User user;

        [SetUp]
        public void Setup()
        {
            user = new User();
            user.Email = "votantai4899@gmail.com";
            user.Username = "votantai4899";
            user.Password = "tantai4899";
            user.Name = "Võ Tấn Tài";
            user.Status = true;
            user.Role = "admin";
        }

        [Test]
        public void LoginTest()
        {
            Assert.IsNotNull(dao.CheckLogin(user));
        }
    }
}