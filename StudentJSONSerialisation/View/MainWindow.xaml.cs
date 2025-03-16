using Microsoft.Win32;
using StudentJSONSerialisation.Entyties;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace StudentJSONSerialisation.View
{
    public partial class MainWindow : Window
    {
        private string fileName = $"Students.json";

        public static CreateStudentWindow? CreateStudWindow;
        public static CreateGroupWindow? CreateGroupWindow;
        public static SetGroupWindow? SetGroupWindow;
        public static DeleteStudentWindow? DeleteStudentWindow;

        public static ObservableCollection<Student>? Students { get; set; }
        public static ObservableCollection<Group>? Groups { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            Students = new ObservableCollection<Student>();
            Groups = new ObservableCollection<Group>();
            DataContext = this;

            Students.CollectionChanged += Students_CollectionChanged;
            Student.GroupChanged += (s, e) => StudentSerialise();
        }

        private void Students_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                StudentSerialise();
            }
        }

        private void StudentSerialise()
        {
            string studentJson = JsonSerializer.Serialize(Students, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(fileName, studentJson);
        }

        private void CreateStudentButton(object sender, RoutedEventArgs e)
        {
            if (CreateStudWindow == null)
            {
                CreateStudWindow = new CreateStudentWindow();
                CreateStudWindow.Closed += (s, args) => CreateStudWindow = null;
                CreateStudWindow.Show();
            }
        }

        private void LoadButton(object sender, RoutedEventArgs e) 
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "JSON файли (*.json)|*.json|Усі файли (*.*)|*.*"
            };

            if (ofd.ShowDialog() == true)
            {
                string json = File.ReadAllText(ofd.FileName);
                List<Student>? students = JsonSerializer.Deserialize<List<Student>>(json);

                if (students.Count > 0)
                {
                    for (int i = 0; i < students.Count; i++)
                    {
                        Students.Add(students[i]);
                        if (students[i].id > Student._nextId) Student._nextId = students[i].id + 1;
                        if (students[i].Group != null)
                        {
                            Groups.Add(students[i].Group);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Файл порожній або не містить коректних даних.");
                }
            }
        }

        private void ShowStudentButton(object sender, RoutedEventArgs e)
        {
            studentGrid.Visibility = Visibility.Visible;
            groupGrid.Visibility = Visibility.Collapsed;
        }

        private void ShowGroupsButton(object sender, RoutedEventArgs e)
        {
            studentGrid.Visibility = Visibility.Collapsed;
            groupGrid.Visibility = Visibility.Visible;
        }

        private void CreteGroupButton(object sender, RoutedEventArgs e)
        {
            if (CreateGroupWindow == null)
            {
                CreateGroupWindow = new CreateGroupWindow();
                CreateGroupWindow.Closed += (s, args) => CreateGroupWindow = null;
                CreateGroupWindow.Show();
            }
        }

        private void SetGroupButton(object sender, RoutedEventArgs e)
        {
            if (SetGroupWindow == null)
            {
                SetGroupWindow = new SetGroupWindow();
                SetGroupWindow.Closed += (s, args) => SetGroupWindow = null;
                SetGroupWindow.Show();
            }
        }

        private void DeleteStudentButton(object sender, RoutedEventArgs e)
        {
            if (DeleteStudentWindow == null)
            {
                DeleteStudentWindow = new DeleteStudentWindow();
                DeleteStudentWindow.Closed += (s, args) => DeleteStudentWindow = null;
                DeleteStudentWindow.Show();
            }
        }
    }
}