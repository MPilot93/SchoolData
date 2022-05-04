using SchoolLibrary;
using System.Data.SqlClient;

namespace Persister
{
    public class TeacherPersister
    {
        private readonly string ConnectionString;
        public TeacherPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Add(Teacher teacher)
        {
            var sql = @"
                        INSERT INTO [dbo].[Teacher]
                                   ([IdTeacher]
                                   ,[IdPerson]
                                   ,[Matricola]
                                   ,[DataAssunzione]
                                   
                             VALUES
                                   (@IdTeacher
                                   ,@IdPerson
                                   ,@Matricola
                                   ,@DataAssunzione)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdTeacher", teacher.IdTeacher);
            command.Parameters.AddWithValue("@IdPerson", teacher.IdPerson);
            command.Parameters.AddWithValue("@Matricola", teacher.Matricola);
            command.Parameters.AddWithValue("@DataAssunzione", teacher.DataAssunzione);

            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<Teacher> GetTeacher(int IdTeacher)
        {

            var sql = @"
                           SELECT [IdTeacher]
                          ,[IdPerson]
                          ,[Matricola]
                          ,[DataAssunzione]


                      FROM[dbo].[Teacher]";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new Teacher
                {
                    DataAssunzione = Convert.ToDateTime(reader["DataAssunzione"]),
                    IdPerson = Convert.ToInt32((string)reader["IdPerson"]),
                    IdTeacher = Convert.ToInt32(reader["IdTeacher"]),
                    Matricola = reader["Matricola"].ToString(),
                };

            }

        }
    }

}