using SchoolLibrary;
using System.Data.SqlClient;

namespace Persister
{
    public class PersonPersister
    {
        private readonly string ConnectionString;
        public PersonPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Add(Person person)
        {
            var sql = @"
                        INSERT INTO [dbo].[Person]
                                   ([Id]
                                   ,[Name]
                                   ,[Surname]
                                   ,[BirthDay]
                                   ,[Gender]
                                   ,[Address]
                                   
                             VALUES
                                   (@Id
                                   ,@IName
                                   ,@Surname
                                   ,@BirthDay
                                   ,@Gender
                                   ,@Address)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", person.Id);
            command.Parameters.AddWithValue("@Name", person.Name);
            command.Parameters.AddWithValue("@Surname", person.Surname);
            command.Parameters.AddWithValue("@BirthDay", person.Birthday);
            command.Parameters.AddWithValue("@Gender", person.Gender);
            command.Parameters.AddWithValue("@Address", person.Address);

            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<Person> GetPerson(int Id)
        {

            var sql = @"
                           SELECT [Id]
                                  ,[Name]
                                  ,[Surname]
                                  ,[BirthDay]
                                  ,[Gender]
                                  ,[Address]


                      FROM[dbo].[Person]";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new Person
                {
                    
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    Birthday = Convert.ToDateTime(reader["BirtDay"]),
                    Gender = reader["Gender"].ToString(),
                    Address = reader["Address"].ToString(),
                };

            }

        }
    }

}