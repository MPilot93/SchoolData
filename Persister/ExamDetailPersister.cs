using SchoolLibrary;
using System.Data.SqlClient;

namespace Persister
{
    public class ExamDetailPersister
    {
        private readonly string ConnectionString;
        public ExamDetailPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Add(ExamDetails examDetails)
        {
            var sql = @"
                        INSERT INTO [dbo].[ExamDetails]
                                   ([IdExamDetails]
                                   ,[IdExam]
                                   ,[IdStudent]
                             
                                   
                             VALUES
                                   (@IdExamDetails
                                   ,@IdExam
                                   ,@IdStudent";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdExamDetails", examDetails.IdExamDetails);
            command.Parameters.AddWithValue("@IdExam", examDetails.IdExam);
            command.Parameters.AddWithValue("@IdStudent", examDetails.IdStudent);

            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<ExamDetails> GetExamDetails(int IdExamDetails)
        {

            var sql = @"
                           SELECT[IdExamDetails]
                                   ,[IdExam]
                                   ,[IdStudent]
                                   


                      FROM[dbo].[ExamDetails]";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new ExamDetails
                {
                    IdExamDetails = Convert.ToInt32(reader["IdExamDetails"]),
                    IdExam = Convert.ToInt32(reader["IdExam"]),
                    IdStudent = Convert.ToInt32(reader["IdStudent"]),


                };

            }

        }
    }

}