using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab6_7
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int? Job_ID { get; set; }
        public int? Status_ID { get; set; }

        public override string ToString()
        {
            return $"ID: {PersonID}, Name: {Name}, Gender: {Gender}, Age: {Age}, Job: {Job_ID}, Status: {Status_ID}";
        }
    }

    public class PersonDao : IDao<Person>
    {
        private string _connectionString = "Data Source=(local);Initial Catalog=Personnel;Integrated Security=True";

        public List<Person> GetAll()
        {
            List<Person> persons = new List<Person>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Person", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Person person = new Person
                            {
                                PersonID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Gender = reader.GetString(2),
                                Age = reader.GetInt32(3),
                                Job_ID = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                Status_ID = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                            };

                            persons.Add(person);
                        }
                    }
                }
            }

            return persons;
        }

        public Person Get(int id)
        {
            Person person = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Person WHERE PersonID = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            person = new Person
                            {
                                PersonID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Gender = reader.GetString(2),
                                Age = reader.GetInt32(3),
                                Job_ID = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                Status_ID = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                            };
                        }
                    }
                }
            }

            return person;
        }

        public void Add(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Person (Name, Gender, Age, Job_ID, Status_ID) VALUES (@Name, @Gender, @Age, @Job_ID, @Status_ID)", connection))
                {
                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@Gender", person.Gender);
                    command.Parameters.AddWithValue("@Age", person.Age);
                    command.Parameters.AddWithValue("@Job_ID", person.Job_ID.HasValue ? person.Job_ID.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status_ID", person.Status_ID.HasValue ? person.Status_ID.Value : (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE Person SET Name = @Name, Gender = @Gender, Age = @Age, Job_ID = @Job_ID, Status_ID = @Status_ID WHERE ID = @PersonID", connection))
                {
                    command.Parameters.AddWithValue("@PersonID", person.PersonID);
                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@Gender", person.Gender);
                    command.Parameters.AddWithValue("@Age", person.Age);
                    command.Parameters.AddWithValue("@Job_ID", person.Job_ID.HasValue ? person.Job_ID.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status_ID", person.Status_ID.HasValue ? person.Status_ID.Value : (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Person WHERE ID = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
