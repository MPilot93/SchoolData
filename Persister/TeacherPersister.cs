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
                                   ,[Matricola]
                                   ,[Registration]
                                   
                             VALUES
                                   (@IdTeacher
                                   ,@Matricola
                                   ,@Registration)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdTeacher", teacher.IdTeacher);
            command.Parameters.AddWithValue("@Matricola", teacher.Matricola);
            command.Parameters.AddWithValue("@Registration", teacher.Registration);

            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<Teacher> GetTeacher(int IdTeacher)
        {

            var sql = @"
                           [SELECT[IdTeacher]
                          ,[Matricola]
                          ,[Registration]


                      FROM[dbo].[Teacher]";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new Teacher
                {
                    Registration = Convert.ToDateTime(reader["Registration"]),

                    IdTeacher = Convert.ToInt32(reader["IdTeacher"]),
                    Matricola = reader["Matricola"].ToString(),
                };

            }

        }
    }

}