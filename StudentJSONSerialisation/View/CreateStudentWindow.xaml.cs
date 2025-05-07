using StudentJSONSerialisation.Data;
using StudentJSONSerialisation.Entyties;
using System.Windows;

namespace StudentJSONSerialisation.View
{
    public partial class CreateStudentWindow : Window
    {
        string FirstName;
        string LastName;
        int Age;

        public CreateStudentWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FirstName = FirsNameBox.Text.Trim();
            LastName = LastNameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                MessageBox.Show("Ім'я та прізвище не може бути порожньою або складатися лише з пробілів.");
                return;
            }

            if (int.TryParse(AgeBox.Text, out int Age))
            {
                if (Age >= 16 & Age <= 30)
                {
                    MainWindow.Students.Add(Student.CreateStudent(FirstName, LastName, Age));

                    DBService.AddStudent(Student.CreateStudent(FirstName, LastName, Age));

                    MainWindow.Load();
                }
                else MessageBox.Show("Помилка: Дозволений вік від 16 до 30 років");
            }
            else MessageBox.Show("Помилка: Невірний формат віку");
        }
    }
}
