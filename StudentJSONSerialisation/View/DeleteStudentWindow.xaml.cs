using StudentJSONSerialisation.Entyties;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentJSONSerialisation.View
{
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
                if (selectedStudent != null)
                {
                    selectedStudent.Group.RemoveStudent(selectedStudent);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть студента для видалення.");
            }
        }
    }
}
