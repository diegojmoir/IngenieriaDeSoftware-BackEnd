using System;
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
            _moqRepository.Setup(x => x.CreateUserInStorage(_nonCreatedValidUser))
                .Returns(_validFirebaseObject);
            _moqRepository.Setup(x => x.GetUserFromStorageByUserNameAndPassword(ValidUserUsername, ValidUserPassword))
                .Returns(_validFirebaseObject);
            _moqRepository.Setup(x => x.GetUserFromStorageByEmail(ValidUserEmail))
                .Returns(_validFirebaseObject);
            _moqRepository.Setup(x => x.GetUserFromStorageByUsername(ValidUserUsername))
                .Returns(_validFirebaseObject);

            var myMapper=new MapperConfiguration(x => { x.AddProfile(new MappingProfile());}).CreateMapper();
            _userService=new UserService(_moqRepository.Object,myMapper);
        }

        [Test]
        public void Should_return_valid_object_when_creating_new_user_and_new_user_is_valid()
        {
            var result = _userService.CreateUser(_nonCreatedValidUser);
            Assert.IsNotNull(result);
            Assert.AreEqual(_validUserKey,result.Key);
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
            Assert.AreEqual(_validUserKey,result.Key);
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
        public void Should_return_valid_object_if_user_exists_with_given_email()
        {
            var result = _userService.GetUserByEmail(ValidUserEmail);
            Assert.IsNotNull(result);
            Assert.AreEqual(_validUserKey, result.Key);
            Assert.AreEqual(_nonCreatedValidUser.Email, result.Email);
        }

        [Test]
        public void Should_return_null_if_user_doesnt_exist_with_given_username()
        {
            var result = _userService.GetUserByUsername(NonExistentUserUsername);
            Assert.IsNull(result);
        }
        [Test]
        public void Should_return_valid_object_if_user_exists_with_given_username()
        {
            var result = _userService.GetUserByUsername(ValidUserUsername);
            Assert.IsNotNull(result);
            Assert.AreEqual(_validUserKey, result.Key);
            Assert.AreEqual(_nonCreatedValidUser.Username, result.Username);
        }



        [Test]
        public void Should_return_null_when_there_is_no_user_for_password_and_username()
        {
            var result = _userService.GetUser(NonExistentUserUsername,NonExistentUserPassword);
            Assert.IsNull(result);
        }

    }
}
