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
        private UsersController testController;
        private Mock<IUserService> moqUserService;
        private const string nonExistentUserPassword = "*";
        private const string nonExistentUserUsername = "someInvalidUsername";
        private const string ValidUserPassword = "pass";
        private const string ValidUserUsername = "someValidUsername";
        private readonly string ValidUserKey = Guid.NewGuid().ToString();
        private User ValidUser;
        private User NonCreatedValidUser;
        private User InvalidUser;


        [OneTimeSetUp]
        public void BeforeTests() 
        {
            ValidUser = new User
            {
                FirstName="Some FirstName",
                LastName="Some LastName",
                Email="validemail@url.com",
                Key=ValidUserKey,
                Username=ValidUserUsername,
                Password=ValidUserPassword
            };
            NonCreatedValidUser = new User
            {
                FirstName=ValidUser.FirstName,
                LastName=ValidUser.LastName,
                Email=ValidUser.Email,
                Username=ValidUser.Username,
                Password=ValidUser.Password
            };
            InvalidUser = null;
            moqUserService = new Mock<IUserService>();
            moqUserService
                .Setup(x => x.GetUser(ValidUserUsername, ValidUserPassword)).
                Returns(ValidUser);
            moqUserService
                .Setup(x => x.CreateUser(NonCreatedValidUser))
                .Returns(ValidUser);
            
            testController=new UsersController(moqUserService.Object);
        }
        [Test]
        public void Should_return_OkResult_If_username_and_password_exist_for_a_user() 
        {
            var result = testController
                .GetUser(ValidUserUsername, ValidUserPassword) as OkObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Return_object_shall_not_be_null_If_username_and_password_are_valid() 
        {
            var result = testController
                .GetUser(ValidUserUsername, ValidUserPassword) as OkObjectResult;
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf(typeof(User), result.Value);
        }

        [Test]
        public void Should_return_bad_request_if_username_and_password_are_not_valid() 
        {
            var result = testController.GetUser(nonExistentUserUsername, nonExistentUserPassword) as NotFoundResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void Should_return_OkResult_if_new_user_is_valid() 
        {
            var result = testController.Create(NonCreatedValidUser) as OkObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void User_key_shall_be_valid_if_createad_user_is_valid() 
        {
            var result = testController.Create(NonCreatedValidUser) as OkObjectResult;
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(ValidUserKey, ((User)result.Value).Key);
        }

        [Test]
        public void Should_return_bad_request_if_new_user_is_not_valid() 
        {
            var result = testController.Create(InvalidUser) as BadRequestResult;
            Assert.AreEqual(400, result.StatusCode);        
        }
        
        
        
    }
}
