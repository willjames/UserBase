using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UserBase.Integration.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {

        [Test]
        public void Can_insert_new_user_record()
        {
            int userRecordCount;

            var record = new UserRecord()
            {
                FirstName = "test",
                LastName = "name"
            };

            var repo = new UserRepository(); // refactor to use IoC


            // create record
            repo.CreateUser(record);

            // pull record count
            userRecordCount = repo.GetUserRecordCount();

            Assert.That(userRecordCount, Is.GreaterThan(1));
        }
    }

    public class UserRepository
    {
        public UserRepository()
        {
        }

        internal void CreateUser(UserRecord record)
        {
            throw new NotImplementedException();
        }

        internal int GetUserRecordCount()
        {
            throw new NotImplementedException();
        }
    }

    public class UserRecord
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateRegistered { get; set; }

    }
}
