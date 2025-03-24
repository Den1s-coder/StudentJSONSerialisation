using StudentJSONSerialisation.Entyties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace StudentJSONSerialisation.View
{
    /// <summary>
    /// Логика взаимодействия для CreateStudentWindow.xaml
    /// </summary>
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
            }
            else MessageBox.Show("Помилка: Невірний формат віку");
        }
    }
}
