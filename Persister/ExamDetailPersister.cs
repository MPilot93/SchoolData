﻿using SchoolLibrary;
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

        public int Add(ExamDetails examDetails)
        {
            var sql = @"
                        INSERT INTO [dbo].[ExamDetails]
                                   ,[IdExam]
                                   ,[IdStudent]
                             
                                   
                             VALUES
                                   @IdExam
                                   ,@IdStudent";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdExam", examDetails.IdExam);
            command.Parameters.AddWithValue("@IdStudent", examDetails.IdStudent);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public ExamDetails GetExamDetails(int IdExamDetails)
        {
            var sql = @"
                           SELECT[IdExamDetails]
                                   ,[IdExam]
                                   ,[IdStudent]
                                   


                      FROM[dbo].[ExamDetails]";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdSubject", IdSubject);
            var reader = command.ExecuteReader();

            ExamDetails result = null;

            while (reader.Read())
            {
                result = new ExamDetails
                {
                    IdExamDetails = Convert.ToInt32(reader["IdExamDetails"]),
                    IdExam = Convert.ToInt32(reader["IdExam"]),
                    IdStudent = Convert.ToInt32(reader["IdStudent"]),
                };

            }
            return result;

        }
    
        public IEnumerable<ExamDetails> GetExamDetails()
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