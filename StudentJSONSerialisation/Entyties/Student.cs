using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace StudentJSONSerialisation.Entyties
{
    public class Student
    {
        public int id;
        public string FirstName;
        public string LastName;
        public int Age;

        private static int _nextId = 1;

        private Student(string firstName, string lastName, int age)
        {
            id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public static Student? CreateStudent(string firstName, string lastName, int age)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Ім'я та прізвище не може бути порожньою або складатися лише з пробілів.");
                return null;
            }

            return new Student(firstName, lastName, age);
        }
    }
}
