﻿using System.Windows;
using System.Xml.Linq;

namespace StudentJSONSerialisation.Entyties
{
    public class Student
    {
        public int id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Group? Group { get; set; }

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

        public void SetGroup(int groupID) //TODO realise processe set group
        {
        }

        public override string ToString() { return $"{id} {FirstName} {LastName}"; }
    }
}
