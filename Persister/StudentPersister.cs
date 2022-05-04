using SchoolLibrary;
using System.Data.SqlClient;

namespace Persister
{
    public class StudentPersister
    {
        private readonly string ConnectionString;
        public StudentPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Add(Student student)
        {
            var sql = @"
                        INSERT INTO [dbo].[Student]
                                   ([IdStudent]
                                   ,[IdPerson]
                                   ,[Matricola]
                                   ,[DataIscrizione]
                                   
                             VALUES
                                   (@IdStudent
                                   ,@IdPerson
                                   ,@Matricola
                                   ,@DataIscrizione)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdStudent", student.IdStudent);
            command.Parameters.AddWithValue("@IdPerson", student.IdPerson);
            command.Parameters.AddWithValue("@Matricola", student.Matricola);
            command.Parameters.AddWithValue("@DataIscrizione", student.DataIscrizione);
            
            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<Student> GetStudent(int IdStudent)
        {

            var sql = @"
                           SELECT [IdStudent]
                          ,[IdPerson]
                          ,[Matricola]
                          ,[DataIscrizione]


                      FROM[dbo].[Student]";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
           
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new Student
                {
                    DataIscrizione = Convert.ToDateTime(reader["DataIscrizione"]),
                    IdStudent = Convert.ToInt32(reader["IdStudent"]),
                    IdPerson = Convert.ToInt32(reader["IdPerson"]),
                    Matricola = reader["Matricola"].ToString(),
                };

            }

        }
    }

}
//public bool Delete(int Idstudent)
//{
//    var sql = @"DELETE FROM [dbo].[Person]
//                WHERE Id=@Id ";
//    using var connection = new SqlConnection(ConnectionString);
//    connection.Open();
//    using var command = new SqlCommand(sql, connection);
//    command.Parameters.AddWithValue("@Id", 1);
//    return command.ExecuteNonQuery() > 0;
//}


//public bool Update(Person person)
//{
//    var sql = @"UPDATE [dbo].[Person]
//               SET [Name] = @Name
//                  ,[Surname] = @Surname
//                  ,[BirthDay] = @BirthDay
//                  ,[Gender] = @Gender
//             WHERE @Id=Id";

//    using var connection = new SqlConnection(ConnectionString);
//    connection.Open();
//    using var command = new SqlCommand(sql, connection);
//    command.Parameters.AddWithValue("@Name", person.Name);
//    command.Parameters.AddWithValue("@Surname", person.Surname);
//    command.Parameters.AddWithValue("@BirthDay", person.Birthday);
//    command.Parameters.AddWithValue("@Gender", person.Gender);
//    command.Parameters.AddWithValue("@Id", person.Id);
//    return command.ExecuteNonQuery() > 0;
//}
