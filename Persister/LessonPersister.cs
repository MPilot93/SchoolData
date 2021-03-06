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

        public int Add(Lesson lesson)
        {
            var sql = @"
                        INSERT INTO [dbo].[Lesson]
                                    [IdTeacher]
                                   ,[IdSubject]
                              
                             
                                   
                             VALUES
                                    @IdTeacher
                                   ,@IdSubject); SELECT @@IDENTITY AS 'Identity';";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdTeacher", lesson.IdTeacher);
            command.Parameters.AddWithValue("@IdSubject", lesson.IdSubject);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public Lesson GetLesson(int IdLesson)
        {
            var sql = @"
                           SELECT  l.IdLesson, l.IdTeacher, l.IdSubject
                           from Lesson l
                           where l.IdLesson=@IdLesson";       


                      

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdLesson", IdLesson);
            var reader = command.ExecuteReader();
            Lesson result = null;
            while (reader.Read())
            {
                result = new Lesson
                {
                    IdLesson = Convert.ToInt32(reader["IdLesson"]),
                    IdTeacher = Convert.ToInt32(reader["IdTeacher"]),
                    IdSubject = Convert.ToInt32(reader["IdSubject"]),
                };
            }
            return result;
        }
    

        public IEnumerable<Lesson> GetLesson()
        {

            var sql = @"
                           SELECT  l.IdLesson, l.IdTeacher, l.IdSubject
                           from Lesson l";


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