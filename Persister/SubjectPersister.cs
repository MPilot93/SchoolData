using SchoolLibrary;
using System.Data.SqlClient;

namespace Persister
{
    public class SubjectPersister
    {
        private readonly string ConnectionString;
        public SubjectPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int Add(Subject subject)
        {
            var sql = @"
                        INSERT INTO [dbo].[Subject]
                                   [Name]
                                   ,[Description]
                                   ,[Credits]
                                   ,[Hours]
                                   
                             VALUES
                                    @Name
                                   ,@Description
                                   ,@Credits
                                   ,@Hours); SELECT @@IDENTITY AS 'Identity'; ";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", subject.Name);
            command.Parameters.AddWithValue("@Description", subject.Description);
            command.Parameters.AddWithValue("@Credits", subject.Credits);
            command.Parameters.AddWithValue("@Hours", subject.Hours);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public Subject GetSubject(int IdSubject)
        {

            var sql = @"SELECT 
                      s.IdSubject, p.Name, p.Desciption, p.Credits, p.Hours
                      from Subject s
                      where t.IdSubject=@IdSubject";



            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@IdSubject", IdSubject);
            var reader = command.ExecuteReader();
            Subject result = null;
            while (reader.Read())
            {
                result = new Subject
                {
                    IdSubject = Convert.ToInt32(reader["IdSubject"]),
                    Name = Convert.ToString(reader["Name"]),
                    Description = Convert.ToString(reader["Description"]),
                    Credits = Convert.ToInt32(reader["Credits"]),
                    Hours = Convert.ToInt32(reader["Hours"]),
                };

            }
            return result;

        }

        public IEnumerable<Subject> GetSubject()
        {

            var sql = @"SELECT 
                      s.IdSubject, p.Name, p.Desciption, p.Credits, p.Hours
                      from Subject s";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            Subject result = null;
            while (reader.Read())
            {
                yield return new Subject
                {
                    IdSubject = Convert.ToInt32(reader["IdSubject"]),
                    Name = Convert.ToString(reader["Name"]),    
                    Description = Convert.ToString(reader["Description"]),  
                    Credits = Convert.ToInt32(reader["Credits"]),
                    Hours = Convert.ToInt32(reader["Hours"]),

                };

            }

        }
    }

}