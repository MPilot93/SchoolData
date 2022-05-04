
using SchoolLibrary;


namespace Persister
{
    public class HandlerLesson
    {
        private readonly string connectionString = "Server=.;Database=Gestionale;Trusted_Connection=True;"; //LAPTOP-6U512VIE\SQLEXPRESS

        public bool InsertLesson()
        {
            var lesson = new Lesson()
            {
                IdLesson = 1,
                IdTeacher = 1,
                IdSubject = 1,
            
            };

            var persister = new LessonPersister(connectionString);
            return persister.Add(lesson);
        }


    }


}