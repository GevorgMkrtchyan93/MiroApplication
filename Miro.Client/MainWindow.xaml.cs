using Miro.Client.ViewModels;
using Miro.Client.Views;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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

            DataContext = new RegisterViewModel(new Miro.Client.Services.NavigationService<RegisterView>());
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Navigate<RegisterView>();
        }

        public static MainWindow Instance { get; set; }

        public void Navigate<T>()
            where T : UserControl, new()
        {
            content.Content = new T();
        }
    }
}
