using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab6_7
{
    public class Status
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }

        public override string ToString()
        {
            return $"ID: {StatusID}, Name: {StatusName}";
        }
    }

    public class StatusDao : IDao<Status>
    {
        private string _connectionString = "Data Source=(local);Initial Catalog=Personnel;Integrated Security=True";

        public List<Status> GetAll()
        {
            List<Status> statuses = new List<Status>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Status", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Status status = new Status
                            {
                                StatusID = reader.GetInt32(0),
                                StatusName = reader.GetString(1)
                            };

                            statuses.Add(status);
                        }
                    }
                }
            }

            return statuses;
        }

        public Status Get(int id)
        {
            Status status = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Status WHERE ID = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            status = new Status
                            {
                                StatusID = reader.GetInt32(0),
                                StatusName = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            return status;
        }

        public void Add(Status status)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Status (Name) VALUES (@Name)", connection))
                {
                    command.Parameters.AddWithValue("@Name", status.StatusName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Status status)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Status SET Name = @Name WHERE ID = @StatusID", connection))
                {
                    command.Parameters.AddWithValue("@StatusID", status.StatusID);
                    command.Parameters.AddWithValue("@Name", status.StatusName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Status WHERE ID = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
