using BoxContentCommon.Entities;
using BoxContentCommon.Interfaces;
using BoxContentCommon.Mappings;
using BoxContentCommon.Interfaces.Fakes;
using BoxContentCommon.Services;

namespace BoxContentTests.Common.Service
{
    [TestClass]
    public class UserServiceUnitTests
    {
        IBoxContentConfiguration _configuration;
        IUserStorageService _userRepository;

        public UserServiceUnitTests()
        {
            // Register Mongo DB user mappings
            UserMap.Map();

            _configuration = new FakeBoxContentConfiguration();
            _userRepository = new UserStorageService(_configuration);
        }

        public User CreateAndAssertUser(User user)
        {
            var new_user = _userRepository.Create(user);

            Assert.IsNotNull(new_user);
            Assert.IsNotNull(new_user.userID);

            return new_user;
        }

        [TestMethod]
        public void Should_Create_New_User()
        {
            var user = BoxContentTestUtilities.GetDummyUser();
            var new_user = _userRepository.Create(user);

            Assert.IsNotNull(new_user);
            Assert.IsNotNull(new_user.userID);
            Assert.AreEqual(user.name, new_user.name);
            Assert.AreEqual(user.eMail, new_user.eMail);
        }

        [TestMethod]
        public void Should_Not_Create_User_Without_Email()
        {
            var user = BoxContentTestUtilities.GetDummyUser();

            user.eMail = null;

            var new_user = _userRepository.Create(user);

            Assert.IsNull(new_user);
        }

        [TestMethod]
        public void Should_Not_Create_User_Without_Name()
        {
            var user = BoxContentTestUtilities.GetDummyUser();

            user.name = null;

            var new_user = _userRepository.Create(user);

            Assert.IsNull(new_user);
        }

        [TestMethod]
        public void Should_Get_By_Email()
        {
            // 1st - Create user to be found
            var user = BoxContentTestUtilities.GetDummyUser();
            var new_user = CreateAndAssertUser(user);

            // 2nd - Test getting user by email
            var found_user = _userRepository.GetByEmail(user.eMail);

            Assert.IsNotNull(found_user);
            Assert.IsNotNull(found_user.userID);
            Assert.AreEqual(new_user.userID, found_user.userID);
        }


        [TestMethod]
        public void Should_Get_By_ID()
        {
            // 1st - Create user to be found
            var user = BoxContentTestUtilities.GetDummyUser();
            var new_user = CreateAndAssertUser(user);

            // 2nd - Test getting user by email
            var found_user = _userRepository.GetByID(user.userID);

            Assert.IsNotNull(found_user);
            Assert.IsNotNull(found_user.userID);
            Assert.AreEqual(new_user.userID, found_user.userID);
        }

        public void Should_Not_Create_User_With_Duplicated_Email_Address()
        {
            // 1st - Create user            
            var user = BoxContentTestUtilities.GetDummyUser();
            var new_user = CreateAndAssertUser(user);

            // 2nd - Try to create user with existing user email
            var user2 = new User()
            {
                name = "John the Email Collider",
                eMail = user.eMail,
                createdAt = DateTime.UtcNow
            };

            Assert.ThrowsException<Exception>(() => _userRepository.Create(user2));
        }

        //public void Should_Not_Create_User_With_Invalid_Email_Address()
        //{
        //    // todo: this is a business test, should not be here

        //    throw new NotImplementedException();
        //}

        [TestMethod]
        public void Should_Update_User()
        {
            // 1st - Create user            
            var user = BoxContentTestUtilities.GetDummyUser();
            var user_to_update = CreateAndAssertUser(user);

            // 2nd - Update user
            user_to_update.name = "John the Renamed";
            user_to_update.eMail = $"john.renamed@test.{DateTime.UtcNow.Ticks.ToString()}.cl";

            var updated_user = _userRepository.Update(user_to_update);
        }

        //[TestMethod]
        //public void Should_Not_Update_User_With_Duplicated_Email_Address()
        //{
        //    // todo: this is a business test, should not be here

        //    throw new NotImplementedException();
        //}

        [TestMethod]
        public void Should_Delete_User()
        {
            // 1st - Create user            
            var user = BoxContentTestUtilities.GetDummyUser();
            var user_to_delete = CreateAndAssertUser(user);

            var deleted_user = _userRepository.Delete(user_to_delete);

            Assert.IsNotNull(deleted_user);
            Assert.IsNotNull(deleted_user.userID);

            var deleted_user_email_search = _userRepository.GetByEmail(deleted_user.eMail);

            Assert.IsNull(deleted_user_email_search);
        }

        //[TestMethod]
        //public void Should_Delete_User_With_Boxes()
        //{
        //    // todo: this is an integration test, should not be here

        //    throw new NotImplementedException();
        //}
    }
}