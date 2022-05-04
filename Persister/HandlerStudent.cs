
using SchoolLibrary;


namespace Persister
{
    public class HandlerStudent
    {
        private readonly string connectionString = "Server=.;Database=Gestionale;Trusted_Connection=True;"; //LAPTOP-6U512VIE\SQLEXPRESS

        public bool InsertStudent()
        {
            var student = new Student()
            {
                IdStudent = 1,
                Matricola = "1111",
                Registration = new DateTime(2015, 1, 1),
            };

            var persister = new StudentPersister(connectionString);
            return persister.Add(student);
        }

     
    }
    

}