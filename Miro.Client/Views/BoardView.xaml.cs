using Miro.Client.ViewModels;

using System.Windows.Controls;

namespace Miro.Client.Views
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Page
    {
        //private Point startPoint;
        //private Point endPoint;
        //private bool isDrawing = false;
        BoardViewModel ViewModel { get; set; }

        public BoardView(BoardViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
            //this.MouseDown += DrawingCanvas_MouseLeftButtonDown;
            //this.MouseUp += DrawingCanvas_MouseLeftButtonUp;
            //this.MouseMove += DrawingCanvas_MouseMove;
            //ApplicationContext.Instance.ReceivedDrawingEvent += Instance_ReceivedDrawingEvent;
        }

        //private void Instance_ReceivedDrawingEvent(double arg1, double arg2, double arg3, double arg4)
        //{
        //    DrawLine(new Point(arg1, arg2), new Point(arg3, arg4));
        //}

        //private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    isDrawing = true;
        //    startPoint = e.GetPosition(DrawingCanvas);
        //}

        //private async void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    isDrawing = false;
        //    endPoint = e.GetPosition(DrawingCanvas);
        //    await ApplicationContext.Instance.SendDrawingCommand(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        //}

        //private async void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (isDrawing)
        //    {
        //        endPoint = e.GetPosition(DrawingCanvas);
        //        await ApplicationContext.Instance.SendDrawingCommand(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        //        startPoint = endPoint;
        //    }
        //}

        //private void DrawLine(Point start, Point end)
        //{
        //    Dispatcher.Invoke(() =>
        //    {
        //        var _currentLine = new Line();
        //        _currentLine.Stroke = Brushes.Black;
        //        _currentLine.X1 = start.X;
        //        _currentLine.Y1 = start.Y;
        //        _currentLine.X2 = end.X;
        //        _currentLine.Y2 = end.Y;

        //        _currentLine.StrokeThickness = 2;

        //     DrawingCanvas.Children.Add(_currentLine);
        //    });
        //}
    }
}
