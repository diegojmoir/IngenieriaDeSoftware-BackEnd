using System;
using System.Collections.Generic;
using AutoMapper;
using RestauranteAPI.Models;
using RestauranteAPI.Services;
using RestauranteAPI.Repositories.Injections;
using NUnit.Framework;
using Moq;
using Firebase.Database;
using Firebase.Database.Streaming;
using RestauranteAPI.Models.Mapping;

namespace RestaurateAPITests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IUserRepository> _moqRepository;
        private const string NonExistentUserPassword = "*";
        private const string NonExistentUserUsername = "someInvalidUsername";
        private const string NonExistentUserEmail = "invalidemail@url.com";
        private const string ValidUserPassword = "pass";
        private const string ValidUserUsername = "someValidUsername";
        private const string ValidUserEmail = "validemail@url.com";
        private readonly string _validUserKey = Guid.NewGuid().ToString();
        private FirebaseObject<User> _validFirebaseObject;
        private User _nonCreatedValidUser;
        private User _invalidUser;
        private User _validDatabaseModel;

        [OneTimeSetUp]
        public void BeforeEachTest()
        {

            _nonCreatedValidUser = new User
            {
                FirstName="Some FirstName",
                LastName="Some LastName",
                Email= ValidUserEmail,
                Username=ValidUserUsername,
                Password=ValidUserPassword

            };

            _validDatabaseModel=new User
            {
               
                FirstName=_nonCreatedValidUser.FirstName,
                LastName=_nonCreatedValidUser.LastName,
                Email=_nonCreatedValidUser.Email,
                Username=_nonCreatedValidUser.Username,
                Password=_nonCreatedValidUser.Password
            };
            
            _validFirebaseObject=new FirebaseEvent<User>(_validUserKey,_validDatabaseModel
                ,FirebaseEventType.InsertOrUpdate,FirebaseEventSource.Offline);

            _invalidUser = null;
            _moqRepository=new Mock<IUserRepository>();

            _moqRepository.Setup(x => x.GetUserFromStorageByUserNameAndPassword(It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(_nonCreatedValidUser);
            _moqRepository.Setup(x => x.CreateUserInStorage(_nonCreatedValidUser))
                .Returns(_validDatabaseModel);
            var myMapper=new MapperConfiguration(x => { x.AddProfile(new MappingProfile());}).CreateMapper();
            _userService=new UserService(_moqRepository.Object,myMapper);
        }

        [Test]
        public void Should_return_valid_object_when_creating_new_user_and_new_user_is_valid()
        {
            var result = _userService.CreateUser(_nonCreatedValidUser);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Should_return_valid_null_if_object_created_is_not_valid()
        {
            var result = _userService.CreateUser(_invalidUser);
            Assert.IsNull(result);
        }

        [Test]
        public void Should_return_valid_object_if_user_exist_with_valid_password_and_username()
        {
            var result = _userService.GetUser(ValidUserUsername,ValidUserPassword);
            Assert.IsNotNull(result);
            Assert.AreEqual(_nonCreatedValidUser.Username,result.Username);
            Assert.AreEqual(_nonCreatedValidUser.Password,result.Password);
        }
        [Test]
        public void Should_return_null_if_user_doesnt_exist_with_given_email()
        {
            var result = _userService.GetUserByEmail(NonExistentUserEmail);
            Assert.IsNull(result);
        }

        [Test]
        public void Should_return_null_when_there_is_no_user_for_password_and_username()
        {
            _moqRepository.Setup(x => x.GetUserFromStorageByUserNameAndPassword(NonExistentUserUsername,
                NonExistentUserPassword));
            var result = _userService.GetUser(NonExistentUserUsername,NonExistentUserPassword);
            Assert.IsNull(result);
        }


        [Test]
        public void should_return_true_when_user_exist_for_username_or_email()
        {
            _moqRepository.Setup(x => x.GetExistentUsers(ValidUserUsername,
                ValidUserEmail))
                .Returns(new List<User>
                {
                    _validDatabaseModel
                });
            var result = _userService.CheckUserAlreadyExist(ValidUserUsername, ValidUserEmail);
            Assert.IsTrue(result);
        }

        [Test]
        public void should_return_false_when_user_do_not_exist_for_username_or_email()
        {
            _moqRepository.Setup(x => x.GetExistentUsers(NonExistentUserUsername,
                    NonExistentUserEmail))
                .Returns(new List<User>());
            var result = _userService.CheckUserAlreadyExist(NonExistentUserUsername, NonExistentUserEmail);
            Assert.IsFalse(result);
        }

        [Test]
        public void should_return_valid_user_model_when_user_exist_by_email()
        {
            _moqRepository.Setup(x => x.GetUserFromStorageByEmail(ValidUserEmail))
                .Returns(_nonCreatedValidUser);
            var result = _userService.GetUserByEmail(ValidUserEmail);
            Assert.IsTrue(result.Email==ValidUserEmail);
        }


    }
}
