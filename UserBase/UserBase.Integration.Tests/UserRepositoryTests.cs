using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace UserBase.Integration.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        [Test]
        public void Can_get_connection_string_from_app_config()
        {
            Assert.That(connectionString, Is.Not.Empty);
        }

        [Test]
        public void Can_get_user_count()
        {
            var repo = new UserRepository(connectionString); // refactor to use IoC

            var userRecordCount = repo.GetUserRecordCount();

            Assert.That(userRecordCount, Is.GreaterThan(-1));
        }

        [Test, Ignore("WIP")]
        public void Can_insert_new_user_record()
        {
            int userRecordCount;

            var record = new UserRecord()
            {
                FirstName = "test",
                LastName = "name"
            };

            var repo = new UserRepository(connectionString); // refactor to use IoC

            // create record
            repo.CreateUser(record);

            // pull record count
            userRecordCount = repo.GetUserRecordCount();

            Assert.That(userRecordCount, Is.GreaterThan(-1));
        }
    }

    public class UserRepository
    {
        public string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal void CreateUser(UserRecord record)
        {
            var connection = new SqlConnection(_connectionString);
            throw new NotImplementedException();
        }

        public int GetUserRecordCount()
        {
            var userRecordCount = -1;
            var connection = new SqlConnection(_connectionString);

            connection.Open();

            var command = new SqlCommand("SELECT COUNT(*) FROM dbo.UserRecords");
            command.Connection = connection;

            userRecordCount = Convert.ToInt32(command.ExecuteScalar());

            connection.Close();

            return userRecordCount;
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
