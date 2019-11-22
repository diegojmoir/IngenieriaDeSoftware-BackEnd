using RestauranteAPI.Controllers;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Models;
using NUnit.Framework;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Models.Dto;

namespace RestaurateAPITests.Controllers
{
    
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController _testController;
        private Mock<IUserService> _moqUserService;
        private const string NonExistentUserPassword = "*";
        private const string NonExistentUserUsername = "someInvalidUsername";
        private const string ValidUserPassword = "pass";
        private const string ValidUserUsername = "someValidUsername";
        private readonly string _validUserKey = Guid.NewGuid().ToString();
        private UserDto _validUser;
        private User _nonCreatedValidUser;
        private User _invalidUser;
        private User _alreadyExistingEmailUser;
        private User _alreadyExistingUsernameUser;
        private User _emptyUsernameUser;
        private User _emptyFirstNameUser;
        private User _emptyLastNameUser;
        private User _emptyPasswordUser;
        private User _emptyEmailUser;
        private UserDto _conflictingUser;
        private const string AlreadyExisintgEmail = "some.existing@mail";
        private const string AlreadyExistingUsername = "some.existing@username";


        [OneTimeSetUp]
        public void BeforeTests() 
        {
            _emptyUsernameUser = new User
            {
                FirstName = "Some FirstName",
                LastName = "Some LastName",
                Password = "Some Password",
                Email = "Some Email",
                Username = "",
            };

            _emptyFirstNameUser = new User
            {
                FirstName = "",
                LastName = "Some LastName",
                Password = "Some Password",
                Email = "Some Email",
                Username = "Some Username",
            };

            _emptyLastNameUser = new User
            {
                FirstName = "FirstName",
                LastName = "",
                Password = "Some Password",
                Email = "Some Email",
                Username = "Some Username",
            };

            _emptyPasswordUser = new User
            {
                FirstName = "Some FirstName",
                LastName = "Some LastName",
                Password = "",
                Email = "Some Email",
                Username = "Some Username",
            };

            _emptyEmailUser = new User
            {
                FirstName = "Some FirstName",
                LastName = "Some LastName",
                Password = "Some Password",
                Email = "",
                Username = "Some Username",
            };

            _alreadyExistingEmailUser = new User
            {
                FirstName = "Some FirstName",
                LastName = "Some LastName",
                Password = "Some Password",
                Email = AlreadyExisintgEmail,
                Username = "Some UserName",
            };
            _alreadyExistingUsernameUser = new User
            {
                FirstName = "Some Firstname",
                LastName = "Some LastName",
                Password = "Some Password",
                Email = "some@email.com",
                Username = AlreadyExistingUsername
            };
            _conflictingUser = new UserDto
            {
                Key = _validUserKey,
                FirstName = "Some FirstName",
                LastName = "Some LastName",
                Email = AlreadyExisintgEmail,
                Username = AlreadyExistingUsername,
                Password = ValidUserPassword,
            };
            _validUser = new UserDto
            {
                Key = _validUserKey,
                FirstName="Some FirstName",
                LastName="Some LastName",
                Email="validemail@url.com",
                Username=ValidUserUsername,
                Password=ValidUserPassword
            };
            _nonCreatedValidUser = new User
            {
                FirstName=_validUser.FirstName,
                LastName=_validUser.LastName,
                Email=_validUser.Email,
                Username=_validUser.Username,
                Password=_validUser.Password
            };
            _invalidUser = null;
            _moqUserService = new Mock<IUserService>();
            _moqUserService
                .Setup(x => x.GetUser(ValidUserUsername, ValidUserPassword)).
                Returns(_validUser);
            _moqUserService
                .Setup(x => x.CreateUser(_nonCreatedValidUser))
                .Returns(_validUser);
            _moqUserService
                .Setup(x => x.GetUserByEmail(AlreadyExisintgEmail)).
                Returns(_conflictingUser);
            _testController=new UsersController(_moqUserService.Object);
        }
        [Test]
        public void Should_return_OkResult_If_username_and_password_exist_for_a_user() 
        {
            var result = _testController
                .GetUser(ValidUserUsername, ValidUserPassword) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Return_object_shall_not_be_null_If_username_and_password_are_valid() 
        {
            var result = _testController
                .GetUser(ValidUserUsername, ValidUserPassword) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf(typeof(UserDto), result.Value);
        }

        [Test]
        public void Should_return_bad_request_if_username_and_password_are_not_valid() 
        {
            var result = _testController.GetUser(NonExistentUserUsername, NonExistentUserPassword) as NotFoundResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void Should_return_OkResult_if_new_user_is_valid() 
        {
            var result = _testController.Create(_nonCreatedValidUser) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void User_key_shall_be_valid_if_created_user_is_valid() 
        {
            var result = _testController.Create(_nonCreatedValidUser) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(_validUserKey, ((UserDto)result.Value).Key);
        }

        [Test]
        public void Should_return_bad_request_if_new_user_is_not_valid() 
        {
            var result = _testController.Create(_invalidUser) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);        
        }

        [Test]
        public void Should_return_conflict_if_new_user_username_is_already_taken()
        {
            var result = _testController.Create(_alreadyExistingUsernameUser) as ConflictResult;
            Assert.IsNull(result);
        }

        [Test]
        public void Should_return_conflict_if_new_user_email_is_alreay_taken()
        {
            var result = _testController.Create(_alreadyExistingEmailUser) as ConflictResult;
            Assert.IsNull(result);
        }

        [Test]
        public void Should_return_bad_request_if_username_is_empty()
        {
            var result = _testController.Create(_emptyUsernameUser) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Should_return_bad_request_if_firstname_is_empty()
        {
            var result = _testController.Create(_emptyFirstNameUser) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Should_return_bad_request_if_lastname_is_empty()
        {
            var result = _testController.Create(_emptyLastNameUser) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Should_return_bad_request_if_email_is_empty()
        {
            var result = _testController.Create(_emptyEmailUser) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Should_return_bad_request_if_password_is_empty()
        {
            var result = _testController.Create(_emptyPasswordUser) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
