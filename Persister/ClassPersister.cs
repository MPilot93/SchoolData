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
                                    [IdStudent]
                                   ,[IdLesson]
                              
                             
                                   
                             VALUES
                                    @IdStudent
                                   ,@IdLesson;SELECT @@IDENTITY AS 'Identity';";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdStudent", classroom.IdStudent);
            command.Parameters.AddWithValue("@IdLesson", classroom.IdLesson);

            return command.ExecuteNonQuery() > 0;
        }

        public Class GetClass(int IdClass)
        {
            var sql = @"SELECT 
                      c.IdClass, c.IdStudent, c.IdLesson
                      from Class c
                      where c.IdClass=@IdClass";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@IdClass", IdClass);
            var reader = command.ExecuteReader();
            Class result = null;
            while (reader.Read())
            {
                result = new Class
                {
                    IdClass = Convert.ToInt32(reader["IdClass"]),
                    IdStudent = Convert.ToInt32(reader["IdStudent"]),
                    IdLesson = Convert.ToInt32(reader["IdLesson"]),
                };

            }
            return result;
        }
        public IEnumerable<Class> GetClass()
        {

            var sql = @"
                           SELECT
                                c.IdClass, c.IdStudent, c.IdLesson
                                from Class c";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            Class result=null;
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