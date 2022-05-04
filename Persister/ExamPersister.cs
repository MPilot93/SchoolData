using SchoolLibrary;
using System.Data.SqlClient;

namespace Persister
{
    public class ExamPersister
    {
        private readonly string ConnectionString;
        public ExamPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Add(Exam exam)
        {
            var sql = @"
                        INSERT INTO [dbo].[Exam]
                                   ([IdExam]
                                   ,[IdTeacher]
                                   ,[Date]
                                   ,[IdSubject]
                             
                                   
                             VALUES
                                   (@IdExam
                                   ,@IdTeacher
                                   ,@Date
                                   ,@IdSubject)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdExam", exam.IdExam);
            command.Parameters.AddWithValue("@IdTeacher", exam.IdTeacher);
            command.Parameters.AddWithValue("@Date", exam.Date);
            command.Parameters.AddWithValue("@IdSubject", exam.IdSubject);

            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<Exam> GetExam(int IdExam)
        {

            var sql = @"
                           [SELECT[IdExam]
                                   ,[IdTeacher]
                                   ,[Date]
                                   ,[IdSubject]
                                   


                      FROM[dbo].[Exam]";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new Exam
                {
                    IdExam = Convert.ToInt32(reader["IdSubject"]),
                    IdTeacher = Convert.ToInt32(reader["IdTeacher"]),
                    Date = Convert.ToDateTime(reader["Date"]),
                    IdSubject = Convert.ToInt32(reader["IdSubject"]),
                   

                };

            }

        }
    }

}