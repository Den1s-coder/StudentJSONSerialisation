using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace StudentJSONSerialisation.Entyties
{
    public class Group
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        private List<Student> Students { get; set; }

        private static int _nextId = 1;

        [JsonConstructor]
        private Group(string name)
        {
            ID = _nextId++;
            Name = name;
            Students = [];
        }

        public static Group? CreateGroup(string name) 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Назва групи не може бути порожньою або складатися лише з пробілів.");
                return null;
            }

            if (name.Length < 3 || name.Length > 50)
            {
                MessageBox.Show("Назва групи має містити від 3 до 50 символів.");
                return null;
            }

            return new Group(name);
        }
        public void AddStudent(Student student)
        {
            if (student == Students.FirstOrDefault(X => X.id == student.id))
            {
                MessageBox.Show("Цей студент вже присутній в цій групі");
                return;
            }

            if (student.Group != null) 
            {
                student.Group.RemoveStudent(student);
            }
            Students.Add(student);
            MessageBox.Show($"Студента {student.FirstName} {student.LastName} додано до групи {Name}.");
        }

        private void RemoveStudent(Student student) 
        {
            student.Group.Students.Remove(student);
        }

        public void ChangeName(string name) 
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Назва групи не може бути порожньою або складатися лише з пробілів.");
                return;
            }

            if (name.Length < 3 || name.Length > 50)
            {
                MessageBox.Show("Назва групи має містити від 3 до 50 символів.");
                return;
            }

            if (Name == name)
            {
                MessageBox.Show("Нова назва збігається з поточною.");
                return;
            }

            Name = name;
            MessageBox.Show("Назву групи успішно змінено.");
        }

        public override string ToString() { return $"{Name}"; }
    }
}
