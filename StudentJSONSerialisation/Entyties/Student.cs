using System.ComponentModel;
using System.Text.Json.Serialization;

namespace StudentJSONSerialisation.Entyties
{
    public class Student : INotifyPropertyChanged
    {
        public int id;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        private Group? _group { get; set; }
        public Group Group
        {
            get => _group;
            set
            {
                _group = value;
                OnPropertyChanged(nameof(Group));
                GroupChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public static event EventHandler? GroupChanged; 

        public static int _nextId = 1;

        [JsonConstructor]
        public Student(string firstName, string lastName, int age)
        {
            id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public static Student CreateStudent(string firstName, string lastName, int age){ return new Student(firstName, lastName, age);}

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public override string ToString() => $"{id} {FirstName} {LastName}";
    }
}
