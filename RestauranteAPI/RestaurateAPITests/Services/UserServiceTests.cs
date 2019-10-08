﻿using System;
using RestauranteAPI.Models;
using RestauranteAPI.Services;
using RestauranteAPI.Repositories.Injections;
using NUnit.Framework;
using Moq;
using Firebase.Database;
using Firebase.Database.Streaming;

namespace RestaurateAPITests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IUserRepository> _moqRepository;
        private const string NonExistentUserPassword = "*";
        private const string NonExistentUserUsername = "someInvalidUsername";
        private const string ValidUserPassword = "pass";
        private const string ValidUserUsername = "someValidUsername";
        private readonly string _validUserKey = Guid.NewGuid().ToString();
        private FirebaseObject<User> _validFirebaseObject;
        private User _validUser;
        private User _nonCreatedValidUser;
        private User _invalidUser;

        

        [OneTimeSetUp]
        public void BeforeEachTest()
        {
            _validUser = new User
            {
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

            _validFirebaseObject=new FirebaseEvent<User>(_validUserKey,_validUser
                ,FirebaseEventType.InsertOrUpdate,FirebaseEventSource.Offline);

            _invalidUser = null;
            _moqRepository=new Mock<IUserRepository>();
            _moqRepository.Setup(x => x.CreateUserInStorage(_nonCreatedValidUser))
                .Returns(_validFirebaseObject);
            _moqRepository.Setup(x => x.GetUserFromStorageByUserNameAndPassword(ValidUserUsername, ValidUserPassword))
                .Returns(_validFirebaseObject);
            _userService=new UserService(_moqRepository.Object);
        }

        [Test]
        public void Should_return_valid_object_when_creating_new_user_and_new_user_is_valid()
        {
            var result = _userService.CreateUser(_nonCreatedValidUser);
            Assert.IsNotNull(result);
            Assert.AreEqual(_validUser.Key,result.Key);
        }

        [Test]
        public void Should_return_valid_null_if_object_created_is_not_valid()
        {
            var result = _userService.CreateUser(_invalidUser);
            Assert.IsNull(result);
        }

        [Test]
        public void Should_return_valid_object_if_user_exist_whit_valid_password_and_username()
        {
            var result = _userService.GetUser(ValidUserUsername,ValidUserPassword);
            Assert.IsNotNull(result);
            Assert.AreEqual(_validUser.Key,result.Key);
            Assert.AreEqual(_validUser.Username,result.Username);
            Assert.AreEqual(_validUser.Password,result.Password);
        }

        [Test]
        public void Should_return_null_when_there_is_no_user_for_password_and_username()
        {
            var result = _userService.GetUser(NonExistentUserUsername,NonExistentUserPassword);
            Assert.IsNull(result);
        }

    }
}