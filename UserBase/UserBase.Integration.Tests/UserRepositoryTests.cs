using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace UserBase.Integration.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        //odd issue - unable to read connection string from web.config
        //postponing resolution as would be solving wrong problem for purposes of this exercise

        //readonly string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        readonly string connectionString = @"data source=DESKTOP-RUDASU2\SQLEXPRESS;Initial Catalog=UserBase;Integrated Security=SSPI;";

        //[Test]
        //public void Can_get_connection_string_from_web_config()
        //{
        //    Assert.That(connectionString, Is.Not.Empty);
        //}

        [Test, Ignore("WIP")]
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
