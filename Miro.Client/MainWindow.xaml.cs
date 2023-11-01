using Miro.Client.ViewModels;
using Miro.Client.Views;

using System.Windows;

using NavigationService = Miro.Client.Services.NavigationService;

namespace Miro.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; }
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = viewModel;
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Loaded(MainFrame);
        }
    }
}
