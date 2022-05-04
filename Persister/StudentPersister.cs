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

        public int Add(Student student)
        {
            var sql = @"
                        INSERT INTO [dbo].[Student]
                                   ([IdPerson]
                                   ,[Matricola]
                                   ,[DataIscrizione])
                                   
                             VALUES
                                   (@IdPerson
                                   ,@Matricola
                                   ,@DataIscrizione);SELECT @@IDENTITY AS 'Identity';";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdPerson", student.Id);
            command.Parameters.AddWithValue("@Matricola", student.Matricola);
            command.Parameters.AddWithValue("@DataIscrizione", student.DataIscrizione);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public Student GetStudent(int IdStudent)
        {

            var sql = @"select p.Id,p.Name,p.Gender,p.Surname,p.BirthDay,p.Address,s.IdStudente,s.DataIscrizione,s.Matricola from Person p 
                        join Student s on p.Id = s.IdPerson where s.IdStudente=@IdStudente";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@IdStudente", IdStudent);
            var reader = command.ExecuteReader();
            Student result = null;
            while (reader.Read())
            {
                result = new Student
                {
                    Address = reader["Address"].ToString(),
                    Birthday = Convert.ToDateTime(reader["BirthDay"]),
                    Gender = reader["Gender"].ToString(),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    DataIscrizione = Convert.ToDateTime(reader["DataIscrizione"]),
                    IdStudent = Convert.ToInt32(reader["IdStudente"]),
                    Id = Convert.ToInt32(reader["Id"]),
                    Matricola = reader["Matricola"].ToString(),
                };

            }
            return result;

        }


        public IEnumerable<Student> GetStudent()
        {

            var sql = @"select p.Id,p.Name,p.Gender,p.Surname,p.BirthDay,p.Address,s.IdStudente,s.DataIscrizione,s.Matricola from Person p 
                        join Student s on p.Id = s.IdPerson";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            Student result = null;
            while (reader.Read())
            {
                yield return new Student
                {
                    Address = reader["Address"].ToString(),
                    Birthday = Convert.ToDateTime(reader["BirthDay"]),
                    Gender = reader["Gender"].ToString(),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    DataIscrizione = Convert.ToDateTime(reader["DataIscrizione"]),
                    IdStudent = Convert.ToInt32(reader["IdStudente"]),
                    Id = Convert.ToInt32(reader["Id"]),
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
