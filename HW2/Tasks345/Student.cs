using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }

        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public Student(string name, string jmbag, Gender gender)
        {
            Name = name;
            Jmbag = jmbag;
            this.Gender = gender;
        }

        public override bool Equals(object obj)
        {
            if (obj is Student)
            {
                Student other = (Student)obj;
                return this.Jmbag.Equals(other.Jmbag);
            }
            return false;
            
        }

        public override int GetHashCode()
        {
            return this.Jmbag.GetHashCode();
        }
    }
}
