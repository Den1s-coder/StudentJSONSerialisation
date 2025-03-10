using StudentJSONSerialisation.Entyties;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace StudentJSONSerialisation.View
{
    public partial class MainWindow : Window
    {
        public static CreateStudentWindow? CreateWindow;
        public static ChangeStudentWindow? ChangeWindow;

        public static ObservableCollection<Student>? Students { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            Students = new ObservableCollection<Student>();
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

            string fileName = $"Students.json";
            File.WriteAllText(fileName, studentJson);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CreateWindow == null)
            {
                CreateWindow = new CreateStudentWindow();
                CreateWindow.Closed += (s, args) => CreateWindow = null;
                CreateWindow.Show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ChangeWindow == null) 
            {
                ChangeWindow = new ChangeStudentWindow();
                ChangeWindow.Closed += (s, args) => CreateWindow = null;
                ChangeWindow.Show();
            }
        }
    }
}