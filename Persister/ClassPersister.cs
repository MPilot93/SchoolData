using SchoolLibrary;
using System.Data.SqlClient;

namespace Persister
{
    public class ClassPersister
    {
        private readonly string ConnectionString;
        public ClassPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Add(Class classroom)
        {
            var sql = @"
                        INSERT INTO [dbo].[Class]
                                   ([IdClass]
                                   ,[IdStudent]
                                   ,[IdLesson]
                              
                             
                                   
                             VALUES
                                   (@IdClass
                                   ,@IdStudent
                                   ,@IdLesson)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdClass", classroom.IdClass);
            command.Parameters.AddWithValue("@IdStudent", classroom.IdStudent);
            command.Parameters.AddWithValue("@IdLesson", classroom.IdLesson);

            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<Class> GetClass(int IdClass)
        {

            var sql = @"
                           SELECT[IdClass]
                                   ,[IdStudent]
                                   ,[IdLesson]
                                   


                      FROM[dbo].[Class]";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new Class
                {
                    IdClass = Convert.ToInt32(reader["IdClass"]),
                    IdStudent = Convert.ToInt32(reader["IdStudent"]),
                    IdLesson = Convert.ToInt32(reader["IdLesson"])
                };

            }

        }
    }

}