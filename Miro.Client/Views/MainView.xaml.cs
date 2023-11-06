using Miro.Client.Models;
using Miro.Client.ViewModels;

using System.Windows;
using System.Windows.Controls;

namespace Miro.Client.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Page
    {
        public MainWindowViewModel ViewModel { get; set; }
        public MainView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel = viewModel;
        }
    }
}
