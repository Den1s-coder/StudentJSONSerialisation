using StudentJSONSerialisation.Entyties;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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