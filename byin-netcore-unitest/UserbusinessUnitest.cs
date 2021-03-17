using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Options;
using byin_netcore_transver;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using byin_netcore_business.UseCases.UserBusiness;
using byin_netcore_business.Interfaces;

namespace byin_netcore_unitest
{
    [TestClass]
    public class UserbusinessUnitest
    {
        [TestMethod]
        public async Task ShouldReturnUser_When_Authenticate_CorrectUser()
        {
            var email = "aaa@gmail.com";
            var password = "eeeaaaaasd";
            var roles = new List<string> { "admin", "customer", "visitor" };
            var user = new IdentityUser
            {
                Id = "123",
                Email = email,
                EmailConfirmed = true,
                PasswordHash = "sdfsdljslfksjld",
            };
            var appSetting = new AppSettings
            {
                Secret = "Qwerty123456!@#$%^"
            };
            var passwordHasherMock = new Mock<IPasswordHasher<IdentityUser>>(); 
            var appSettingMock = new Mock<IOptions<AppSettings>>();
            var userStore = new Mock<IUserStore<IdentityUser>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(userStore.Object, null, passwordHasherMock.Object, null, null, null, null, null, null);
            var authorizationServiceMock = new Mock<IAuthorizationBusiness>();
            passwordHasherMock.Setup(ph => ph.VerifyHashedPassword(user, user.PasswordHash, password)).Returns(PasswordVerificationResult.Success);
            userManagerMock.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(user);
            userManagerMock.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
            userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);
            appSettingMock.Setup(a => a.Value).Returns(appSetting);

            var userReturned = await new UserBusiness(appSettingMock.Object, userManagerMock.Object, authorizationServiceMock.Object).AuthenticateAsync(email, password);

            Assert.AreEqual(user.Id, userReturned.Id);
            Assert.AreEqual(null, userReturned.Password);
            CollectionAssert.AreEqual(roles, userReturned.Roles);
            Assert.IsNotNull(userReturned.Token);
        }

        [TestMethod]
        public async Task ShouldReturnNull_When_Authenticate_User_HaveNotBeenConfirmed()
        {
            var email = "aaa@gmail.com";
            var password = "eeeaaaaasd";
            var roles = new List<string> { "admin", "customer", "visitor" };
            var user = new IdentityUser
            {
                Id = "123",
                Email = email,
                EmailConfirmed = true,
                PasswordHash = "sdfsdljslfksjld",
            };
            var appSetting = new AppSettings
            {
                Secret = "Qwerty123456!@#$%^"
            };
            var passwordHasherMock = new Mock<IPasswordHasher<IdentityUser>>();
            var userStore = new Mock<IUserStore<IdentityUser>>();
            var appSettingMock = new Mock<IOptions<AppSettings>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(userStore.Object, null, passwordHasherMock.Object, null, null, null, null, null, null);
            var authorizationServiceMock = new Mock<IAuthorizationBusiness>();

            passwordHasherMock.Setup(ph => ph.VerifyHashedPassword(user, user.PasswordHash, password)).Returns(PasswordVerificationResult.Success);
            userManagerMock.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(user);
            userManagerMock.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(false);
            appSettingMock.Setup(a => a.Value).Returns(appSetting);

            var userReturned = await new UserBusiness(appSettingMock.Object, userManagerMock.Object, authorizationServiceMock.Object).AuthenticateAsync(email, password);

            Assert.IsNull(userReturned);
        }

        [TestMethod]
        public async Task ShouldReturnNull_When_Authenticate_User_NotExist()
        {
            var email = "aaa@gmail.com";
            var password = "eeeaaaaasd";
            var roles = new List<string> { "admin", "customer", "visitor" };
            var user = new IdentityUser
            {
                Id = "123",
                Email = email,
                EmailConfirmed = true,
                PasswordHash = "sdfsdljslfksjld",
            };
            var appSetting = new AppSettings
            {
                Secret = "Qwerty123456!@#$%^"
            };
            var passwordHasherMock = new Mock<IPasswordHasher<IdentityUser>>();
            var userStore = new Mock<IUserStore<IdentityUser>>();
            var appSettingMock = new Mock<IOptions<AppSettings>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(userStore.Object, null, passwordHasherMock.Object, null, null, null, null, null, null);
            var authorizationServiceMock = new Mock<IAuthorizationBusiness>();

            userManagerMock.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync((IdentityUser)null);
            appSettingMock.Setup(a => a.Value).Returns(appSetting);

            var userReturned = await new UserBusiness(appSettingMock.Object, userManagerMock.Object, authorizationServiceMock.Object).AuthenticateAsync(email, password);

            Assert.IsNull(userReturned);
        }

        [TestMethod]
        public async Task ShouldReturnNull_When_Authenticate_UserWith_IncorrectPassword()
        {
            var email = "aaa@gmail.com";
            var password = "eeeaaaaasd";
            var roles = new List<string> { "admin", "customer", "visitor" };
            var user = new IdentityUser
            {
                Id = "123",
                Email = email,
                EmailConfirmed = true,
                PasswordHash = "sdfsdljslfksjld",
            };
            var appSetting = new AppSettings
            {
                Secret = "Qwerty123456!@#$%^"
            };
            var passwordHasherMock = new Mock<IPasswordHasher<IdentityUser>>();
            var userStore = new Mock<IUserStore<IdentityUser>>();
            var appSettingMock = new Mock<IOptions<AppSettings>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(userStore.Object, null, passwordHasherMock.Object, null, null, null, null, null, null);
            var authorizationServiceMock = new Mock<IAuthorizationBusiness>();

            passwordHasherMock.Setup(ph => ph.VerifyHashedPassword(user, user.PasswordHash, password)).Returns(PasswordVerificationResult.Failed);
            userManagerMock.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(user);
            appSettingMock.Setup(a => a.Value).Returns(appSetting);

            var userReturned = await new UserBusiness(appSettingMock.Object, userManagerMock.Object, authorizationServiceMock.Object).AuthenticateAsync(email, password);

            Assert.IsNull(userReturned);
        }
    }
}
