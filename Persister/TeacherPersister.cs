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

        public int Add(Teacher teacher)
        {

      
            var sql = @"


                        INSERT INTO [dbo].[Teacher]
                                   ([IdPerson]
                                   ,[Matricola]
                                   ,[DataAssunzione]
                                   
                             VALUES
                                   (@IdPerson
                                   ,@Matricola
                                   ,@DataAssunzione); SELECT @@IDENTITY AS 'Identity'; ";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdPerson", teacher.Id);
            command.Parameters.AddWithValue("@Matricola", teacher.Matricola);
            command.Parameters.AddWithValue("@DataAssunzione", teacher.DataAssunzione);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        //ottiene un solo insegnante
        public Teacher GetTeacher(int IdTeacher)
        {

            var sql = @"SELECT 
                        p.Id,p.Name,p.Gender,p.Surname,p.BirthDay,p.Address,
                        t.IdPerson,t.Matricola,p.DataAssunzione   
                        from Person p join Teacher t on p.Id = t.IdPerson 
                        where t.IdTeacher=@IdTeacher";



            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@IdTeacher", IdTeacher);
            var reader = command.ExecuteReader();
            Teacher result = null;
            while (reader.Read())
            {
                result = new Teacher
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Address = reader["Address"].ToString(),
                    Birthday = Convert.ToDateTime(reader["BirthDay"]),
                    Gender = reader["Gender"].ToString(),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    DataAssunzione = Convert.ToDateTime(reader["DataAssunzione"]),
                    IdTeacher = Convert.ToInt32(reader["IdTeacher"]),
                    Matricola = reader["Matricola"].ToString(),
                };

            }
            return result;

        }

        //ottiene tutti i teacher
        public IEnumerable<Teacher> GetTeacher()
        {

            var sql = @"select 
                      p.Id,p.Name,p.Gender,p.Surname,p.BirthDay,p.Address,
                      t.IdTeacher,t.DataAssunzione,t.Matricola 
                      from Person p  join Student s on p.Id = s.IdPerson";


            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            var reader = command.ExecuteReader();
            Teacher result = null;
            while (reader.Read())
            {
                yield return new Teacher
                {
                    Address = reader["Address"].ToString(),
                    Birthday = Convert.ToDateTime(reader["BirthDay"]),
                    Gender = reader["Gender"].ToString(),
                    Name = reader["Name"].ToString(),
                    Surname = reader["Surname"].ToString(),
                    DataAssunzione = Convert.ToDateTime(reader["DataAssunzione"]),
                    IdTeacher = Convert.ToInt32(reader["IdTeacher"]),
                    Id = Convert.ToInt32(reader["Id"]),
                    Matricola = reader["Matricola"].ToString(),
                };

            }
        }

    }

}