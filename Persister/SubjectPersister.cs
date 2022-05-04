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

        public bool Add(Subject subject)
        {
            var sql = @"
                        INSERT INTO [dbo].[Subject]
                                   ([IdSubject]
                                   ,[Name]
                                   ,[Description]
                                   ,[Credits]
                                   ,[Hours]
                                   
                             VALUES
                                   (@IdSubject
                                   ,@Name
                                   ,@Description
                                   ,@Credits
                                   ,@Hours)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdSubject", subject.IdSubject);
            command.Parameters.AddWithValue("@Name", subject.Name);
            command.Parameters.AddWithValue("@Description", subject.Description);
            command.Parameters.AddWithValue("@Credits", subject.Credits);
            command.Parameters.AddWithValue("@Hours", subject.Hours);

            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<Subject> GetSubject(int IdSubject)
        {

            var sql = @"
                           SELECT[IdSubject]
                                   ,[Name]
                                   ,[Description]
                                   ,[Credits]
                                   ,[Hours]


                      FROM[dbo].[Suject]";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
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