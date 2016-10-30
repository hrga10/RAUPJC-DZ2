using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4;
using Task5;

namespace Task3
{
    class Program
{
    static void Main(string[] args)
    {
        int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
        string[] strings = integers.Select(i => String.Format("Broj {0} ponavlja se {1} puta ", i, integers.Count(j => j == i))).Distinct().ToArray();

        foreach (String s in strings)
        {
            Console.WriteLine(s);
        }

        // strings [0] = Broj 1 ponavlja se 1 puta
        // strings [1] = Broj 2 ponavlja se 3 puta
        // strings [2] = Broj 3 ponavlja se 2 puta
        // strings [3] = Broj 4 ponavlja se 1 puta
        // strings [4] = Broj 5 ponavlja se 1 puta

        var list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 ") ,
                new Student (" Ivan ", jmbag :" 001234567 ")
            };
        // 2 :(
        var distinctStudents = list.Distinct().Count();
        // 1 :)
        Console.WriteLine(distinctStudents);

        University[] universities = GetAllCroatianUniversities();
        Console.WriteLine("Svi studenti:");
        Student[] allCroatianStudents = universities.SelectMany(u => u.Students).Distinct().ToArray();
        foreach (Student s in allCroatianStudents)
        {
            Console.WriteLine(s.Name);
        }

        Student[] croatianStudentsOnMultipleUniversities = universities.SelectMany(u => u.Students).Where(s => universities.SelectMany(u => u.Students).Count(st => st.Jmbag.Equals(s.Jmbag)) >= 2).Distinct().ToArray();
        Console.WriteLine("Studenti koji studiraju na 2 ili više fakulteta:");
        foreach (Student s in croatianStudentsOnMultipleUniversities)
        {
            Console.WriteLine(s.Name);
        }

        Student[] studentsOnMaleOnlyUniversities = universities.Where(u => u.Students.Count(s => s.Gender.Equals(Gender.Female)) == 0).SelectMany(un => un.Students).Distinct().ToArray();
        Console.WriteLine("Studenti koji studiraju fakultetima na kojim nema žena:");
        foreach (Student s in studentsOnMaleOnlyUniversities)
        {
            Console.WriteLine(s.Name);
        }

        Console.ReadKey();

    }

    public static University[] GetAllCroatianUniversities()
    {
        Student s1 = new Student("Pero", "123", Gender.Male);
        Student s2 = new Student("Ante", "456", Gender.Male);
        Student s3 = new Student("Ivona", "789", Gender.Female);
        Student s4 = new Student("Marija", "987", Gender.Female);
        Student s5 = new Student("Slavko", "654", Gender.Male);
        Student s6 = new Student("Tea", "321", Gender.Female);
        Student s7 = new Student("Mihaela", "012", Gender.Female);
        University[] univ = new University[3];
        univ[0] = new University("Zagreb", s1, s2);
        univ[1] = new University("Split", s2, s3, s4, s5);
        univ[2] = new University("Rijeka", s6, s7);
        return univ;
    }
}
}
