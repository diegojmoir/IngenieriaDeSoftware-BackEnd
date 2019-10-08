using RestauranteAPI.Controllers;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Models;
using NUnit.Framework;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc;

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
        private User _validUser;
        private User _nonCreatedValidUser;
        private User _invalidUser;


        [OneTimeSetUp]
        public void BeforeTests() 
        {
            _validUser = new User
            {
                FirstName="Some FirstName",
                LastName="Some LastName",
                Email="validemail@url.com",
                Key=_validUserKey,
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
            Assert.IsInstanceOf(typeof(User), result.Value);
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
            Assert.AreEqual(_validUserKey, ((User)result.Value).Key);
        }

        [Test]
        public void Should_return_bad_request_if_new_user_is_not_valid() 
        {
            var result = _testController.Create(_invalidUser) as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);        
        }
    }
}
