using System.Data.SqlClient;
using SchoolLibrary;

namespace Persister
{
    public class StudentPersister
    {
        public bool AddStudent(Student student)
        {
            var sql = @"
                        INSERT INTO [dbo].[Student]
                                   ([IdPerson]
                                   ,[Matricola]
                                   ,[DataIscrizione])
                             VALUES
                                   (@IdPerson
                                   ,@Matricola
                                   ,@DataIscrizione)";

            using var connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdPerson", student.Id);
            command.Parameters.AddWithValue("@Matricola", student.Matricola);
            command.Parameters.AddWithValue("@DataAssunzione", student.DataIscrizione);

            return command.ExecuteNonQuery() > 0;
        }
    }
}
