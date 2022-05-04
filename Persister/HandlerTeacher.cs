
using SchoolLibrary;

namespace Persister
{
    public class HandlerTeacher
    {

            private readonly string connectionString = "Server=.;Database=Gestionale;Trusted_Connection=True;"; //LAPTOP-6U512VIE\SQLEXPRESS

            public bool InsertStudent()
            {
                var teacher = new Teacher()
                {
                    IdTeacher = 1,
                    Matricola = "2222",
                    Registration = new DateTime(2006, 1, 1),
                };

                var persister = new TeacherPersister(connectionString);
                return persister.Add(teacher);
            }


        }


    }
}
}
