using Microsoft.AspNet.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Exceptions;
using TraderOop.Domain.Models;
using TraderOop.Domain.Services;
using TraderOop.Domain.Services.AuthenticationServices;

namespace Trader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {

        private Mock<IPasswordHasher> _mockPasswordHasher;
        private Mock<IAccountService> _mockAccountService;
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPasswordHasher.Object);
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            //Arrange
            string expectedUsername = "username";
            string password = "password";
            
            _mockAccountService.Setup(x => x.GetByUsername(expectedUsername)).ReturnsAsync(new Account()
            {
                AccountHolder = new User()
                {
                    Username = expectedUsername
                }
            });
        
            
            _mockPasswordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);
            

            //Act
            Account account = await _authenticationService.Login(expectedUsername, password);

            //Assert
            string actualUsername = account.AccountHolder.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Login_WithIncorrectPasswordForExistingUsername_ThrowsInvalidPasswordException()
        {
            //Arrange
            string expectedUsername = "username";
            string password = "password";
            _mockAccountService.Setup(x => x.GetByUsername(expectedUsername)).ReturnsAsync(new Account()
            {
                AccountHolder = new User()
                {
                    Username = expectedUsername
                }
            });
            _mockPasswordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            //Act
            InvalidPasswordException invalidPasswordException = Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUsername, password));

            //Assert
            string actualUsername = invalidPasswordException.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Login_WithNonExistingUsername_ThrowsUserNotFoundException()
        {
            //Arrange
            string expectedUsername = "username";
            string password = "password";
            _mockPasswordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            //Act
            UserNotFoundException userNotFoundException = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password));

            //Assert
            string actualUsername = userNotFoundException.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            //Arrange
            string password = "password";
            string confirmedPassword = "password2";
            RegistrationResult expectedResult = RegistrationResult.PasswordsDoNotMatch;

            //Act
            RegistrationResult actualResult = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmedPassword);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExists()
        {
            //Arrange
            string email = "test@gmail.com";
            _mockAccountService.Setup(x => x.GetByEmail(email)).ReturnsAsync(new Account());
            RegistrationResult expectedResult = RegistrationResult.EmailAlreadyExists;

            //Act
            RegistrationResult actualResult = await _authenticationService.Register(email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsUsernameAlreadyExists()
        {
            //Arrange
            string username = "test";
            _mockAccountService.Setup(x => x.GetByUsername(username)).ReturnsAsync(new Account());
            RegistrationResult expectedResult = RegistrationResult.UsernameAlreadyExists;

            //Act
            RegistrationResult actualResult = await _authenticationService.Register(It.IsAny<string>(), username, It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnsSuccess()
        {
            //Arrange          
            RegistrationResult expectedResult = RegistrationResult.Success;

            //Act
            RegistrationResult actualResult = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
