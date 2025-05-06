using StudentJSONSerialisation.Data;
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
            name = NameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Назва групи не може бути порожньою або складатися лише з пробілів.");
                return;
            }

            if (name.Length < 3 || name.Length > 50)
            {
                MessageBox.Show("Назва групи має містити від 3 до 50 символів.");
                return;
            }

            MainWindow.Groups.Add(Group.CreateGroup(name));

            DBService.AddGroup(Group.CreateGroup(name));

            MainWindow.Load();
        }
    }
}
