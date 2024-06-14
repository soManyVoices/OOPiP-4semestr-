using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Lab6
{
    public class Job
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}";
        }
    }

    public class JobDao : IDao<Job>
    {
        private string _connectionString = "Data Source=(local);Initial Catalog=Personnel;Integrated Security=True";

        public List<Job> GetAll()
        {
            List<Job> jobs = new List<Job>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Job", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Job job = new Job
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };

                            jobs.Add(job);
                        }
                    }
                }
            }

            return jobs;
        }

        public Job Get(int id)
        {
            Job job = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Job WHERE ID = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            job = new Job
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            return job;
        }

        public void Add(Job job)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Job (Name) VALUES (@Name)", connection))
                {
                    command.Parameters.AddWithValue("@Name", job.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Job job)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Job SET Name = @Name WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", job.ID);
                    command.Parameters.AddWithValue("@Name", job.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Job WHERE ID = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
