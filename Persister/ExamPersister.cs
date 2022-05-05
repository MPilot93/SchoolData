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

        public int Add(Exam exam)
        {
            var sql = @"
                        INSERT INTO [dbo].[Exam]
                                   ,[IdTeacher]
                                   ,[Date]
                                   ,[IdSubject]
                             
                                   
                             VALUES
                                    @IdTeacher
                                   ,@Date
                                   ,@IdSubject); SELECT @@IDENTITY AS 'Identity'; ";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdTeacher", exam.IdTeacher);
            command.Parameters.AddWithValue("@Date", exam.Date);
            command.Parameters.AddWithValue("@IdSubject", exam.IdSubject);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public Subject GetExam(int IdExam)
        {

            var sql = @"SELECT 
                      e.IdExam, e.IdTeacher, e.Date, e.IdSubject
                      from Exam e
                      where e.IdExamt=@IdExam";



            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@IdExam", IdExam);
            var reader = command.ExecuteReader();
            Exam result = null;
            while (reader.Read())
            {
                result = new Exam
                {
                    IdExam = Convert.ToInt32(reader["IdSubject"]),
                    IdTeacher = Convert.ToInt32(reader["IdTeacher"]),
                    Date = Convert.ToDateTime(reader["Date"]),
                    IdSubject = Convert.ToInt32(reader["IdSubject"]),
                };

            }
            return result;

        }

        public IEnumerable<Exam> GetExam(int IdExam)
        {

            var sql = @"
                           SELECT[IdExam]
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