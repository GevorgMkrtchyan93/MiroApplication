using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.Views;

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Miro.Client.ViewModels
{
    public class BoardViewModel:NotifyPropertyChange
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDataService _userDataService;
        private bool IsDrawing = false;
        private Point _startPoint;

        private ObservableCollection<Point> _points = new ObservableCollection<Point>();
        public ObservableCollection<Point> Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        private Point StartPoint
        {
            get => _startPoint;
            set
            {
                _startPoint = value;
                OnPropertyChanged(nameof(StartPoint));
            }
        }

        private Point _endPoint;

        private Point EndPoint
        {
            get => _endPoint;
            set
            {
                _startPoint = value;
                OnPropertyChanged(nameof(EndPoint));
            }
        }

        private int _boardId;
        private int BoardId
        {
            get => _boardId;
            set
            {
                _boardId = value;
                OnPropertyChanged(nameof(BoardId));    
            }
        }

        private int _email;

        public int Email
        {
            get => _email;
            set
            {
                _email = value;

                OnPropertyChanged(nameof(Email));
            }
        }

        public BoardViewModel(INavigationService navigationService,IUserDataService userDataService)
        {
            _navigationService = navigationService;
            _userDataService = userDataService;

            ExitBoard = new CommandService(CanExecute_ExitBoard,Execute_ExitBoard);
        }

        public ICommand ExitBoard { get; set; }

        public ICommand StartDrawingCommand { get; }

        public ICommand EndDrawingCommand { get; }

        public ICommand DrawCommand { get; }

        private bool CanExecute_ExitBoard(object parameter)
        {
            return true;
        }

        private async void Execute_ExitBoard(object parameter)
        {
            try
            {
                _navigationService.NavigateTo(typeof(AccountView));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                throw ex;
            }
        }

        private void StartDrawing(object parameter)
        {
        }

        private void EndDrawing(object parameter)
        {
        }

        private void Draw(object parameter)
        {
            if (parameter is MouseEventArgs e && e.LeftButton == MouseButtonState.Pressed)
            {
                Points.Add(e.GetPosition((UIElement)e.Source));
            }
        }

        private void DrawLine(Point start, Point end)
        {
            //Dispatcher.Invoke(() =>
            //{
            //    var _currentLine = new Line();
            //    _currentLine.Stroke = Brushes.Black;
            //    _currentLine.X1 = start.X;
            //    _currentLine.Y1 = start.Y;
            //    _currentLine.X2 = end.X;
            //    _currentLine.Y2 = end.Y;

            //    _currentLine.StrokeThickness = 2;

            //    DrawingCanvas.Children.Add(_currentLine);
            //});
        }
    }
}
