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

            var record = new UserRecord()
            {
                FirstName = "test",
                LastName = "name",
                Email = "test@testmail.com"
            };

            // create record
            repo.CreateUser(record);

            // pull record count
            userRecordCount = repo.GetUserRecordCount();

            Assert.That(userRecordCount, Is.GreaterThan(0));
        }
    }

    public class UserRepository
    {
        public string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateUser(UserRecord record)
        {
            //ID and DateRegistered field population is offloaded to the DB

            var commandText = $"INSERT INTO UserRecords (FirstName, LastName, Email) " +
                $"VALUES ('{record.FirstName}','{record.LastName}','{record.Email}')"; 

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(commandText))
            {
                connection.Open();
                command.Connection = connection;

                command.ExecuteNonQuery();
            }

        }

        public void TearDownTestData()
        {
            var commandText = $"DELETE FROM UserRecords WHERE FirstName = 'test'";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(commandText))
            {
                connection.Open();
                command.Connection = connection;

                command.ExecuteNonQuery();
            }
        }

        public int GetUserRecordCount()
        {
            var userRecordCount = -1;

            using (var connection = new SqlConnection(_connectionString))
            using(var command = new SqlCommand("SELECT COUNT(*) FROM dbo.UserRecords"))
            {
                connection.Open();
                command.Connection = connection;

                userRecordCount = Convert.ToInt32(command.ExecuteScalar());
            }

            return userRecordCount;
        }


    }
     
    public class UserRecord
    {
        public Guid Id { get; set; }
        public DateTime DateRegistered { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
