using Microsoft.AspNetCore.SignalR.Client;
using Miro.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Miro.Client.Views
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        private Line _currentLine;
        private Point startPoint;
        private Point endPoint;
        private bool isDrawing = false;

        public BoardView()
        {
            InitializeComponent();
        }

        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            startPoint = e.GetPosition(DrawingCanvas);
        }

        private async void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            endPoint = e.GetPosition(DrawingCanvas);
            DrawLine(startPoint, endPoint);
            await ApplicationContext .Instance.SendDrawingCommand(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                endPoint = e.GetPosition(DrawingCanvas);
                DrawLine(startPoint, endPoint);
                startPoint = endPoint;
            }
        }

        private void DrawLine(Point start, Point end)
        {
            _currentLine = new Line();
            _currentLine.Stroke = Brushes.Black;
            _currentLine.X1 = start.X;
            _currentLine.Y1 = start.Y;
            _currentLine.X2 = end.X;
            _currentLine.Y2 = end.Y;

            _currentLine.StrokeThickness = 2;

            DrawingCanvas.Children.Add(_currentLine);
        }
    }
}
