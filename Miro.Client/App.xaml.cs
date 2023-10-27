using System.Windows;

namespace Miro.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Client.MainWindow.Instance = new MainWindow();
            Client.MainWindow.Instance.Show();
        }
    }
}