using SchoolLibrary;
using Persister;

var constring = "Server=.;Database=Gestionale;Trusted_Connection=True;";

var person = new Person {
    Address = "vicolo corto",
    Birthday = new DateTime(1995, 5, 28),
    Gender ="Male",
    Name ="Michele",
    Surname ="De Michelis"
};


var persisterPerson = new PersonPersister(constring);
person.Id = persisterPerson.Add(person);

var person2 = new Person
{
    Address = "vicolo lungo",
    Birthday = new DateTime(1992, 1, 15),
    Gender = "Female",
    Name = "Anna",
    Surname = "Rossi"
};

var persisterPerson2 = new PersonPersister(constring);
person2.Id = persisterPerson.Add(person2);


var person3 = new Person
{
    Address = "strada Dante",
    Birthday = new DateTime(1986, 2, 7),
    Gender = "Male",
    Name = "Paolo",
    Surname = "Neri"
};

var persisterPerson3 = new PersonPersister(constring);
person3.Id = persisterPerson.Add(person3);

var student = new Student
{
    Address = person.Address,
    Birthday = person.Birthday,
    Gender = person.Gender,
    Id = person.Id,
    DataIscrizione = DateTime.Now,
    Matricola = "ABC1234DEF",
    Name = person.Name,
    Surname=person.Surname,
};

var persisterStudent = new StudentPersister(constring);
student.IdStudent = persisterStudent.Add(student);

var teacher = new Teacher
{
    Address = person2.Address,
    Birthday = person2.Birthday,
    Gender = person2.Gender,
    Id = person2.Id,
    Name = person2.Name,
    Surname = person2.Surname,
    DataAssunzione = new DateTime(2010, 10, 1),
    Matricola = "AAA111",
};

var persisterTeacher = new TeacherPersister(constring);
teacher.IdTeacher = persisterTeacher.Add(teacher);


var teacher2 = new Teacher
{
    Address = person3.Address,
    Birthday = person3.Birthday,
    Gender = person3.Gender,
    Id = person3.Id,
    Name = person3.Name,
    Surname = person3.Surname,
    DataAssunzione = new DateTime(2002, 5, 19),
    Matricola = "BB222",
};

var persisterTeacher2 = new TeacherPersister(constring);
teacher2.IdTeacher = persisterTeacher.Add(teacher2);


var subject = new Subject
{
    Name = "Analisi",
    Description = "Corso di analisi1",
    Credits = 12,
    Hours = 60,
};

var persisterSubject = new SubjectPersister(constring);
subject.IdSubject = persisterSubject.Add(subject);

var subject2 = new Subject
{
    Name = "Dati",
    Description = "Corso di dati e sistemi",
    Credits = 9,
    Hours = 50,
};

var persisterSubject2 = new SubjectPersister(constring);
subject2.IdSubject = persisterSubject.Add(subject2);

var exam = new Exam
{
    IdTeacher = teacher.IdTeacher,
    Date = DateTime.Now,
    IdSubject = subject.IdSubject,
};

var persisterExam = new ExamPersister(constring);
exam.IdExam = persisterExam.Add(exam);

var exam2 = new Exam
{
    IdTeacher = teacher2.IdTeacher,
    Date = new DateTime(2022,6,10),
    IdSubject = subject2.IdSubject,
};

var persisterExam2 = new ExamPersister(constring);
exam2.IdExam = persisterExam.Add(exam2);



var allPerson = persisterPerson.GetPerson().ToList();
var allStudent = persisterStudent.GetStudent().ToList();







Console.WriteLine("Ciao");