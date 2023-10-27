using Miro.Client.Views;

using System.Windows;
using System.Windows.Controls;

namespace Miro.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Navigate<LoginView>();
        }

        public static MainWindow Instance { get; set; }

        public void Navigate<T>()
            where T : UserControl, new()
        {
            content.Content = new T();
        }
    }
}
