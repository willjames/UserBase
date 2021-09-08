using NUnit.Framework;
using System.Configuration;
using UserBase.Features.User;

namespace UserBase.Integration.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        UserRepository repo; 

        [SetUp]
        public void SetUp()
        {
            repo = new UserRepository(connectionString); // refactor to use IoC
            repo.TearDownTestData();
        }

        [TearDown]
        public void TearDown()
        {
            repo.TearDownTestData();
        }

        [Test]
        public void Can_get_user_count()
        {
            var userRecordCount = repo.GetUserRecordCount();

            Assert.That(userRecordCount, Is.GreaterThan(-1));
        }

        [Test]
        public void Can_insert_new_user_record()
        {
            int userRecordCount;
            var testUserRecord = GetTestUserRecord();

            // create record
            repo.CreateUser(testUserRecord);

            // pull record count
            userRecordCount = repo.GetUserRecordCount();

            Assert.That(userRecordCount, Is.GreaterThan(0));
        }

        [Test]
        public void Can_get_user_records()
        {
            const int expectedRecordCount = 1;
            var testUserRecord = GetTestUserRecord();

            // create record
            repo.CreateUser(testUserRecord);

            //get records from DB
            var userRecords = repo.GetAllUserRecords();//would use slice for large data set

            Assert.That(userRecords.Count, Is.EqualTo(expectedRecordCount));
        }

        private static UserRecord GetTestUserRecord()
        {
            return new UserRecord()
            {
                FirstName = "test",
                LastName = "name",
                Email = "test@testmail.com"
            };
        }
    }
}
