using StudentJSONSerialisation.Entyties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace StudentJSONSerialisation.View
{
    /// <summary>
    /// Логика взаимодействия для DeleteStudentWindow.xaml
    /// </summary>
    public partial class DeleteStudentWindow : Window
    {
        public ObservableCollection<Student> Students { get; set; }
        public Student selectedStudent { get; set; }

        public DeleteStudentWindow()
        {
            InitializeComponent();

            Students = MainWindow.Students;

            DataContext = this;
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            if (selectedStudent != null)
            {
                Students.Remove(selectedStudent);
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть студента для видалення.");
            }
        }
    }
}
