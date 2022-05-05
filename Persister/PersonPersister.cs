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

        public int Add(Person person)
        {
            var sql = @"
                        INSERT INTO [dbo].[Person]
                                   ([Name]
                                   ,[Surname]
                                   ,[BirthDay]
                                   ,[Gender]
                                   ,[Address])
                                   
                             VALUES
                                   (@Name
                                   ,@Surname
                                   ,@BirthDay
                                   ,@Gender
                                   ,@Address); SELECT @@IDENTITY AS 'Identity';  ";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", person.Name);
            command.Parameters.AddWithValue("@Surname", person.Surname);
            command.Parameters.AddWithValue("@BirthDay", person.Birthday);
            command.Parameters.AddWithValue("@Gender", person.Gender);
            command.Parameters.AddWithValue("@Address", person.Address);

            return Convert.ToInt32(command.ExecuteScalar());
        }


        public bool Delete(int Id)
        {
            var sql = @"DELETE FROM [dbo].[Person]
                        WHERE Id=@Id ";
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", 1);
            return command.ExecuteNonQuery() > 0;
        }

        public Person GetPerson(int Id)
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

            command.Parameters.AddWithValue("@Id", Id);
            var reader = command.ExecuteReader();
            Person result = null;
            while (reader.Read())
            {
                result = new Person
                {

                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    Birthday = Convert.ToDateTime(reader["BirtDay"]),
                    Gender = reader["Gender"].ToString(),
                    Address = reader["Address"].ToString(),
                };
            }
            return result;
        }
        public IEnumerable<Person> GetPerson()
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