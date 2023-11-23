using Miro.Client.ViewModels;

using System.Windows;
using System.Windows.Controls;

namespace Miro.Client.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        LoginViewModel ViewModel { get; set; }
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ApplicationContext.Instance.Username = usernameTextBox.Text;
            //MainWindow.Instance.Navigate<MainView>();
        }
    }
}
