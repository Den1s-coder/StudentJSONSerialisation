using StudentJSONSerialisation.Entyties;
using System.Windows;

namespace StudentJSONSerialisation.View
{
    public partial class CreateGroupWindow : Window
    {
        string name;
        public CreateGroupWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            name = NameTextBox.Text;
            MainWindow.Groups.Add(Group.CreateGroup(name));
        }
    }
}
