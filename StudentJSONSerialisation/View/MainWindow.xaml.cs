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

        public static CreateStudentWindow? CreateWindow;
        public static ChangeStudentWindow? ChangeWindow;

        public static ObservableCollection<Student>? Students { get; set; }
        public static ObservableCollection<Group>? Groups { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            Students = new ObservableCollection<Student>();
            Groups = new ObservableCollection<Group>();
            DataContext = this;

            Students.CollectionChanged += Students_CollectionChanged;
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
            if (CreateWindow == null)
            {
                CreateWindow = new CreateStudentWindow();
                CreateWindow.Closed += (s, args) => CreateWindow = null;
                CreateWindow.Show();
            }
        }

        private void LoadButton(object sender, RoutedEventArgs e) //TODO load student json
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
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

        }
    }
}