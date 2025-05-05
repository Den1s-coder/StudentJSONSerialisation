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
                MainWindow.Students.Add(Student.CreateStudent(FirstName, LastName, Age));

                DBService.AddStudent(Student.CreateStudent(FirstName, LastName, Age));
            }
            else MessageBox.Show("Помилка: Невірний формат віку");
        }
    }
}
