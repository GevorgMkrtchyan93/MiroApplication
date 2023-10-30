using Miro.Client.Models;

using System.Windows;
using System.Windows.Controls;

namespace Miro.Client.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private async void NewBoardButtonClick(object sender, RoutedEventArgs e)
        {
            ApplicationContext.Instance.Connect();
            await ApplicationContext.Instance.SendAsync();

            MainWindow.Instance.Navigate<BoardView>();
        }

        private async void JoinBoardClick(object sender, RoutedEventArgs e)
        {
            ApplicationContext.Instance.Connect();
            ApplicationContext.Instance.SelectedBoardId = int.Parse(boardIdTextBox.Text);
            await ApplicationContext.Instance.SendAsync();
            MainWindow.Instance.Navigate<BoardView>();
        }
    }
}
