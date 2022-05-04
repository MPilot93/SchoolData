
using SchoolLibrary;


namespace Persister
{
    public class HandlerSubject
    {
        private readonly string connectionString = "Server=.;Database=Gestionale;Trusted_Connection=True;"; //LAPTOP-6U512VIE\SQLEXPRESS

        public bool InsertSubject()
        {
            var subject = new Subject()
            {
                IdSubject = 1,
                Name = "Subject1",
                Description = "Subject 1 description",
                Credits = 12,
                Hours = 60,
            };

            var persister = new SubjectPersister(connectionString);
            return persister.Add(subject);
        }


    }


}
