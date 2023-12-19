using Miro.Client.ViewModels;
using System.Windows.Controls;

namespace Miro.Client.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Page
    {
        RegisterViewModel ViewModel { get; }
        public RegisterView(RegisterViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = viewModel;
        }
    }
}
