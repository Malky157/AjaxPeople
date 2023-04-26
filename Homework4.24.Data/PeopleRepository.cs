using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4._24.Data
{
    public class PeopleRepository
    {
        private string _connection { get; set; }
        public PeopleRepository(string connection)
        {
            _connection = connection;
        }
        public void AddPerson(Person person)
        {
            using var connection = new SqlConnection(_connection);
            using var command = connection.CreateCommand();
            command.CommandText = $@"INSERT INTO People(FirstName, LastName, Age)
                                  VALUES(@firstName, @lastName, @age)";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public List<Person> GetPeople()
        {
            using var connection = new SqlConnection(_connection);
            using var command = connection.CreateCommand();
            command.CommandText = $@"SELECT * FROM People
                                  ORDER By Age Desc";
            connection.Open();
            var reader = command.ExecuteReader();
            List<Person> people = new();
            while (reader.Read())
            {
                people.Add(new Person()
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }
        public Person GetPerson(int id)
        {
            using var connection = new SqlConnection(_connection);
            using var command = connection.CreateCommand();
            command.CommandText = $@"SELECT * FROM People
                                  Where Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Person()
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                };
            }
            return null;    
        }
        public void UpdatePerson(Person person)
        {
            using var connection = new SqlConnection(_connection);
            using var command = connection.CreateCommand();
            command.CommandText = $@"Update People
                                  Set FirstName = @firstName,
                                  LastName = @lastName,
                                  Age = @age
                                  Where Id = @id";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            command.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            command.ExecuteNonQuery();
        }
        public void DeletePerson(int id)
        {
            using var connection = new SqlConnection(_connection);
            using var command = connection.CreateCommand();
            command.CommandText = $@"Delete People                                  
                                  Where Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
