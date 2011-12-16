using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimulationPhys2_5_WPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        struct CAProperty
        {
            readonly byte _rule;
            readonly int _length;
            readonly double _density;
            readonly int _maxTime;
            static readonly CAProperty _empty;

            public byte Rule
            {
                get { return _rule;}
            }
            public int Length
            {
                get { return _length;}
            }
            public double Density
            {
                get { return _density; }
            }
            public int MaxTime
            {
                get { return _maxTime; }
            }

            static CAProperty()
            {
                _empty = new CAProperty(0, 0, 0, 0);
            }

            public CAProperty(byte rule, int length, double density, int maxTime)
            {
                _rule = rule;
                _length = length;
                _density = density;
                _maxTime = maxTime;
            }

            static public CAProperty Empty
            {
                get { return _empty; }
            }
        }

        static readonly int boxSize = 10;
        static readonly Pen boxStroke = new Pen(Brushes.LightGray, 0.2);
        SimulationPhys2_5.LinearCellularAutomaton ca;
        CAProperty currentProperty;

        public MainWindow()
        {
            InitializeComponent();
            if (boxStroke.CanFreeze)
                boxStroke.Freeze();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            byteRuleNumberBox.Text = "184";
            lengthBox.Text = "20";
            densityBox.Text = "0.3";
            timeBox.Text = "20";
        }

        private void RedrawButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateCAProperties();

            //canvas.Width = (boxSize + 1) * length + 5;
            //canvas.Height = (boxSize + 1) * time + 5;

            ca = new SimulationPhys2_5.LinearCellularAutomaton(currentProperty.Rule, currentProperty.Length, currentProperty.Density);

            using (var dc = drawingGroup.Open())
            {
                Draw(dc, ca, currentProperty.MaxTime);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.Filter = "PNG file (*.png)|*.png";
            saveDialog.DefaultExt = "png";
            if (!saveDialog.ShowDialog().Value)
                return;

            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                Draw(dc, ca, currentProperty.MaxTime);
            }
            var renderTarget = new RenderTargetBitmap((int)dv.Drawing.Bounds.Width, (int)dv.Drawing.Bounds.Height, 96, 96, PixelFormats.Pbgra32);
            renderTarget.Render(dv);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTarget));
            using (var stream = System.IO.File.Create(saveDialog.FileName))
            {
                encoder.Save(stream);
            }
        }

        void UpdateCAProperties()
        {
            byte rule;
            if (!byte.TryParse(byteRuleNumberBox.Text, out rule))
                currentProperty = CAProperty.Empty;
            int length;
            if (!int.TryParse(lengthBox.Text, out length))
                currentProperty = CAProperty.Empty;
            if (length < 3)
                length = 3;
            double density;
            if (!double.TryParse(densityBox.Text, out density))
                currentProperty = CAProperty.Empty;
            int time;
            if (!int.TryParse(timeBox.Text, out time))
                currentProperty = CAProperty.Empty;

            currentProperty = new CAProperty(rule, length, density, time);
            lengthBox.Text = length.ToString();
        }

        void Draw(DrawingContext dc, SimulationPhys2_5.LinearCellularAutomaton ca, int time)
        {
            ca.Reset();

            foreach (int i in Enumerable.Range(0, time))
            {
                var currentStatus = ca.CurrentState.ToArray();
                foreach (int j in Enumerable.Range(0, ca.Length))
                {
                    if (currentStatus[j])
                        dc.DrawRectangle(Brushes.Black, boxStroke, new Rect(boxSize * j, boxSize * i, boxSize, boxSize));
                    else
                        dc.DrawRectangle(Brushes.White, boxStroke, new Rect(boxSize * j, boxSize * i, boxSize, boxSize));
                }
                ca.Next();
            }
        }
    }
}
