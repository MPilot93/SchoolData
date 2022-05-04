namespace SchoolLibrary
{
    public class Student : Person
    {
        public int IdStudent { get; set; }
        public int IdPerson { get; set; }
        public string Matricola { get; set; }

        public DateTime DataIscrizione { get; set; }

    }
}