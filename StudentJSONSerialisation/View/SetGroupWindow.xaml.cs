using StudentJSONSerialisation.Entyties;
using System.Collections.ObjectModel;
using System.Windows;

namespace StudentJSONSerialisation.View
{
    /// <summary>
    /// Логика взаимодействия для SetGroupWindow.xaml
    /// </summary>
    public partial class SetGroupWindow : Window
    {
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Group> Groups { get; set; }

        public Student selectedStudent { get; set; }
        public Group selectedGroup { get; set; }

        public SetGroupWindow()
        {
            InitializeComponent();

            Students = MainWindow.Students;
            Groups = MainWindow.Groups;

            DataContext = this;

        }

        private void ChangeGroupButton(object sender, RoutedEventArgs e)
        {
            if (selectedStudent != null && selectedGroup != null)
            {
                selectedStudent.Group = selectedGroup; 
                selectedGroup.AddStudent(selectedStudent);
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть студента і групу.");
            }
        }
    }
}
