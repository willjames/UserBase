using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace UserBase.Features.User
{
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

        public List<UserRecord> GetAllUserRecords()
        {
            var recordList = new List<UserRecord>();

            var commandText = $"SELECT Id, FirstName, LastName, Email, DateRegistered FROM UserRecords";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(commandText))
            {
                connection.Open();
                command.Connection = connection;

                //command.ExecuteNonQuery();
                DataTable userRecordTable = new DataTable();

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(userRecordTable);

                    foreach (DataRow row in userRecordTable.Rows)
                    {
                        var record = new UserRecord
                        {
                            Id = new Guid(row["Id"].ToString()),
                            FirstName = row["FirstName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            Email = row["Email"].ToString(),
                            DateRegistered = Convert.ToDateTime(row["DateRegistered"].ToString()),
                        };

                        recordList.Add(record);
                    }
                }
            }

            return recordList;
        }
    }
}