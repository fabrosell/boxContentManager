using BoxContentCommon.Entities;
using BoxContentCommon.Interfaces;
using BoxContentCommon.Interfaces.Fakes;
using BoxContentCommon.Mappings;
using BoxContentCommon.Services;

namespace BoxContentTests.Common.Service
{
    [TestClass]
    public class BoxesServiceUnitTests
    {
        IBoxContentConfiguration _configuration;
        IUserStorageService _userRepository;
        IBoxStorageService _boxRepository;

        User _user;
        User _alternate_user;

        public BoxesServiceUnitTests()
        {
            // Register Mongo DB mappings
            UserMap.Map();
            BoxMap.Map();

            _configuration = new FakeBoxContentConfiguration();
            _userRepository = new UserStorageService(_configuration);
            _boxRepository = new BoxStorageService(_configuration);

            _user = CreateUser();
            _alternate_user = CreateUser();
        }

        public User CreateUser()
        {
            var user = BoxContentTestUtilities.GetDummyUser();

            var new_user = _userRepository.Create(user);

            return new_user;
        }

        public Box CreateAndAssertBox()
        {
            var box = BoxContentTestUtilities.GetDummyBox(_user);

            var new_box = _boxRepository.Create(box);

            Assert.IsNotNull(new_box);
            Assert.IsNotNull(new_box.boxID);
            Assert.IsNotNull(new_box.ownerID);
            Assert.IsNotNull(new_box.items);
            Assert.AreNotEqual(0, new_box.items.Count());

            return new_box;
        }

        [TestMethod]
        public void Should_Create_New_Box()
        {
            var box = BoxContentTestUtilities.GetDummyBox(_user);

            var new_box = _boxRepository.Create(box);

            Assert.IsNotNull(new_box);
            Assert.IsNotNull(new_box.boxID);
            Assert.IsNotNull(new_box.ownerID);
            Assert.AreEqual(box.name, new_box.name);
            Assert.AreEqual(box.items?.Count(), new_box.items?.Count());
        }

        [TestMethod]
        public void Should_Not_Create_Box_Without_Name()
        {
            var box = BoxContentTestUtilities.GetDummyBox(_user);
            box.name = null;

            var new_box = _boxRepository.Create(box);

            Assert.IsNull(new_box);
        }

        [TestMethod]
        public void Should_Search_By_Name()
        {
            var box = CreateAndAssertBox();

            var found_box = _boxRepository.Search(new BoxSearchSettings()
            {
                exactMatch = true,
                searchTerm = box.name,
                searchType = BoxSearchType.SearchByName
            }, _user);

            Assert.IsNotNull(found_box);
            Assert.AreEqual(found_box?.Count(), 1);
            Assert.AreEqual(found_box?[0].boxID, box.boxID);
        }

        [TestMethod]
        public void Should_Get_By_ID()
        {
            var box = CreateAndAssertBox();

            var found_box = _boxRepository.GetByID(box.boxID);

            Assert.IsNotNull(found_box);
            Assert.AreEqual(found_box.boxID, box.boxID);
            Assert.AreEqual(found_box.ownerID, _user.userID);
        }

        public void Should_Not_Create_Box_With_Duplicated_Name_For_User()
        {
            var box = CreateAndAssertBox();

            var box2 = BoxContentTestUtilities.GetDummyBox(_user);
            box2.name = box.name;

            var new_box = _boxRepository.Create(box2);

            Assert.IsNull(new_box);
        }

        [TestMethod]
        public void Should_Update_Box()
        {
            var box = CreateAndAssertBox();

            box.name = $"Box renamed {DateTime.UtcNow.Ticks.ToString()}";
            box.items.Add("Item added on update");

            var updated_box = _boxRepository.Update(box);

            Assert.IsNotNull(updated_box);
            Assert.AreEqual(updated_box.boxID, box.boxID);

            var reloaded_box = _boxRepository.GetByID(box.boxID);

            Assert.IsNotNull(reloaded_box);
            Assert.IsNotNull(reloaded_box.items);
            Assert.AreEqual(reloaded_box.items.Count, box.items.Count);
        }

        //[TestMethod]
        //public void Should_Update_Box_Not_Owned()
        //{
        //    var box = CreateAndAssertBox();

        //    box.name = $"Box renamed {DateTime.UtcNow.Ticks.ToString()}";
        //    box.items.Add("Item added on update");

        //    // Box is owned by _user not _alternate_user
        //    var updated_box = _boxRepository.Update(box, _alternate_user);

        //    Assert.IsNotNull(updated_box);
        //    Assert.AreEqual(updated_box.boxID, box.boxID);

        //    var reloaded_box = _boxRepository.GetByID(box.boxID, _user);

        //    Assert.IsNotNull(reloaded_box);
        //    Assert.IsNotNull(reloaded_box.items);
        //    Assert.AreEqual(reloaded_box.items.Count, box.items.Count);
        //}

        [TestMethod]
        public void Should_Delete_Box()
        {
            var box = CreateAndAssertBox();

            var deleted_box = _boxRepository.Delete(box);

            Assert.IsNotNull(deleted_box);
            Assert.IsNotNull(deleted_box.boxID);

            var deleted_box_search = _boxRepository.GetByID(box.boxID);

            Assert.IsNull(deleted_box_search);
        }

        //    [TestMethod]
        //    public void Should_Not_Delete_Box_Not_Owned()
        //    {
        //        var box = CreateAndAssertBox();

        //        // Box is owned by _user not _alternate_user
        //        var deleted_box = _boxRepository.Delete(box, _alternate_user);

        //        Assert.IsNull(deleted_box);

        //        var box_still_there = _boxRepository.GetByID(box.boxID, _user);

        //        Assert.IsNotNull(box_still_there);
        //    }
    }
}


