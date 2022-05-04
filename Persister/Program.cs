using SchoolLibrary;
using Persister;



var person = new Person {
    Address = "vicolo corto",
    Birthday = new DateTime(1995, 5, 28),
    Gender ="Male",
    Name ="Michele",
    Surname ="De Michelis"
};


var constring = HandlerStudent.GetConnectionString();

var persisterPerson = new PersonPersister(constring);
person.Id = persisterPerson.Add(person);

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

var allStudent = persisterStudent.GetStudent().ToList();

var otherstudent = persisterStudent.GetStudent(student.IdStudent);

var test = student == otherstudent;





Console.WriteLine("Ciao");