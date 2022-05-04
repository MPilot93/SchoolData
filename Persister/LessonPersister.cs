using SchoolLibrary;
using System.Data.SqlClient;

namespace Persister
{
    public class LessonPersister
    {
        private readonly string ConnectionString;
        public LessonPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Add(Lesson lesson)
        {
            var sql = @"
                        INSERT INTO [dbo].[Lesson]
                                   ([IdLesson]
                                   ,[IdTeacher]
                                   ,[IdSubject]
                              
                             
                                   
                             VALUES
                                   (@IdLesson
                                   ,@IdTeacher
                                   ,@IdSubject)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdLesson", lesson.IdLesson);
            command.Parameters.AddWithValue("@IdTeacher", lesson.IdTeacher);
            command.Parameters.AddWithValue("@IdSubject", lesson.IdSubject);

            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<Lesson> GetLesson(int IdLesson)
        {

            var sql = @"
                           SELECT[IdLesson]
                                   ,[IdTeacher]
                                   ,[IdSubject]
                                   


                      FROM[dbo].[Lesson]";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new Lesson
                {
                    IdLesson = Convert.ToInt32(reader["IdLesson"]),
                    IdTeacher = Convert.ToInt32(reader["IdTeacher"]),
                    IdSubject = Convert.ToInt32(reader["IdSubject"])
                };

            }

        }
    }

}