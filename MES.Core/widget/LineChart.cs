using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ColorM = System.Windows.Media.Color;
using PointD = System.Windows.Point;

namespace MES.Core.widget
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MES.Core.widget"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MES.Core.widget;assembly=MES.Core.widget"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:LineChart/>
    ///
    /// </summary>
    public class LineChart : FrameworkElement
    {

        #region 私有字段
        private const int MAX_RENDER_POINTS = 10000;
        private double maxValue;
        private int power;
        private double minValue;
        private readonly VisualCollection _children;
        private int downIndex;
        private int displayCount;
        private const int MIN_POINT_COUNT = 50;

        //静态变量
        private static readonly Pen pen = new()
        {
            Brush = Brushes.Red,
            EndLineCap = PenLineCap.Round,
            StartLineCap = PenLineCap.Round,
            Thickness = 2
        };
        private static readonly Pen dashPen = new()
        {
            DashStyle = new DashStyle { Dashes = new DoubleCollection { 5, 5 } },
            Thickness = 1,
            Brush = Brushes.Gray
        };
        private static readonly Pen greenDashPen = new()
        {
            DashStyle = new DashStyle { Dashes = new DoubleCollection { 5, 5 } },
            Thickness = 1,
            Brush = Brushes.Green
        };

        private static readonly Brush brush = new LinearGradientBrush(ColorM.FromArgb(128, 255, 0, 0),
            Colors.Transparent,
            new PointD(0.5, 0),
            new PointD(0.5, 1));
        #endregion

        #region 依赖属性


        public IEnumerable<PointD> Points
        {
            get { return (IEnumerable<PointD>)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(IEnumerable<PointD>), typeof(LineChart), new PropertyMetadata(default, new PropertyChangedCallback(OnPointsChangedCallback)));

        private static void OnPointsChangedCallback(DependencyObject? d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LineChart host && e.NewValue is List<PointD> _ && host.ActualWidth > 0)
            {
                host.displayCount = host.Points.Count();
                host._children.Clear();
                host._children.Add(host.DrawChart(host, host.Points, host.ActualWidth, host.ActualHeight));
            }
        }



        public IEnumerable<string> Time
        {
            get { return (IEnumerable<string>)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Time.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(IEnumerable<string>), typeof(LineChart), new PropertyMetadata(new List<string>()));



        public string Legend
        {
            get { return (string)GetValue(LegendProperty); }
            set { SetValue(LegendProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Legend.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LegendProperty =
            DependencyProperty.Register("Legend", typeof(string), typeof(LineChart), new PropertyMetadata(""));



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(LineChart), new PropertyMetadata(""));


        #endregion

        #region 构造函数
        static LineChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineChart), new FrameworkPropertyMetadata(typeof(LineChart)));
        }

        public LineChart()
        {
            pen.Freeze();
            dashPen.Freeze();
            greenDashPen.Freeze();
            brush.Freeze();
            _children = new VisualCollection(this);
            MouseMove += OnMouseEnter;
            MouseWheel += OnMouseWheel;
        }
        #endregion

        #region 回调事件
        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if ((displayCount <= MIN_POINT_COUNT && e.Delta > 0) || (displayCount == Points.Count() && e.Delta < 0))
            {
                return;
            }
            var pos = e.GetPosition(this);
            _children.Clear();
            var range = e.Delta switch
            {
                var d when d > 0 => ScaleUpChartPoints(pos),
                var d when d <= 0 => ScaleDownChartPoints(pos),
                _ => throw new ArgumentException("params of mouse event not correct")
            };
            _children.Add(DrawChart(this, range, ActualWidth, ActualHeight));
        }

        private void OnMouseEnter(object? sender, MouseEventArgs e)
        {
            var pos = Mouse.GetPosition(this);
            if (_children.Count > 1)
            {
                for (int i = _children.Count; i > 2; i--)
                {
                    _children.RemoveAt(i - 1);
                }
            }
            _children.Add(DrawPositionLine(pos));
            _children.Add(DrawLegends(pos));
        }

        private IEnumerable<PointD> ScaleUpChartPoints(PointD p)
        {
            var ratio = p.X / ActualWidth;
            displayCount /= 2;
            if (ratio > 0 && ratio < 1)
            {
                downIndex += (int)(displayCount * ratio / 2);
            }
            return Points.Skip(downIndex).Take(displayCount);
        }

        private IEnumerable<PointD> ScaleDownChartPoints(PointD point)
        {
            displayCount *= 2;
            if (displayCount > Points.Count())
            {
                displayCount = Points.Count();
            }
            var ratio = point.X / ActualWidth;
            if (ratio > 0 && ratio < 1)
            {
                downIndex -= (int)(displayCount * ratio / 2);
                if (downIndex < 0) downIndex = 0;
            }
            return Points.Skip(downIndex).Take(displayCount);
        }
        #endregion

        #region 坐标辅助函数
        private static double XCoordination(double width, double x, int totalCount) => width * (x / (totalCount - 1));

        private double YCoordination(double height, double y, double maxValue) => height - (y - minValue) * (height / maxValue) * 0.95f;


        #endregion

        #region 私有成员函数
        private DrawingVisual DrawChart(LineChart chart, IEnumerable<PointD> data, double width, double height)
        {
            var visual = new DrawingVisual();
            var context = visual.RenderOpen();
            var count = data.Count();
            if (count > 1 && ActualHeight > 0 && ActualWidth > 0)
            {
                var Points = data.ToList();
                minValue = Points.Any(d => d.Y < 0) ? Points.Min(d => d.Y) : 0.0;
                var maxValue = Points.Max(d => d.Y) - minValue;
                this.maxValue = maxValue;
                #region 绘制曲线
                var geo = new StreamGeometry();
                using var geoContext = geo.Open();
                var controlX = XCoordination(ActualWidth, 0, count);
                var controlY = YCoordination(ActualHeight, Points.ElementAt(0).Y, maxValue);
                var startPoint = new PointD(controlX, controlY);
                geoContext.BeginFigure(startPoint, true, true);
                var step = count > MAX_RENDER_POINTS ? count / MAX_RENDER_POINTS : 1;
                for (int i = 0; i < count; i += step)
                {
                    var point = new PointD(XCoordination(ActualWidth, i, count), YCoordination(ActualHeight, Points[i].Y, maxValue));
                    geoContext.LineTo(point, true, true);
                }
                context.DrawGeometry(Brushes.Transparent, pen, geo);
                geoContext.LineTo(new PointD(ActualWidth, ActualHeight), false, true);
                geoContext.LineTo(new PointD(0, ActualHeight), false, true);
                geoContext.LineTo(startPoint, false, true);
                context.DrawGeometry(brush, null, geo);
                #endregion
                #region 绘制横坐标
                if (Time != null && Time.Count() == count)
                {
                    var labelCount = Time.Count() > 8 ? 8 : Time.Count();
                    var indexes = Enumerable.Range(1, labelCount - 1).Select(d => Time.Count() / labelCount * d);
                    var labels = indexes.Select(d => Time.ElementAt(d)).ToArray();
                    var labelPositions = Enumerable.Range(1, labelCount - 1).Select(d => new PointD
                    {
                        X = ActualWidth / labelCount * d,
                        Y = ActualHeight - 15
                    }).ToArray();
                    for (var i = 0; i < labelPositions.Length; i++)
                    {
                        var label = labels[i];
                        var pos = labelPositions[i];
                        var formattedText = new FormattedText(label,
                            CultureInfo.GetCultureInfo("en-us"),
                            FlowDirection.LeftToRight,
                            new Typeface("Verdana"),
                            12, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);
                        context.DrawText(formattedText,
                            new PointD(pos.X - (formattedText.Width / 2), pos.Y - (formattedText.Height / 2)));
                    }
                }
                #endregion
                #region 绘制纵坐标
                List<int> yLabels;
                var power = (int)(Math.Floor(Math.Log10(maxValue)));
                this.power = power;
                var factor = maxValue / Math.Pow(10, power);
                if (factor < 4)
                {
                    factor *= 10;
                    power--;
                    yLabels = Enumerable.Range(1, 7).Select(d => (int)Math.Round(factor / 8 * d)).ToList();
                }
                else
                {
                    yLabels = Enumerable.Range(1, (int)factor).Select(d => (int)Math.Round((factor / ((int)factor + 1) * d))).ToList();
                }
                var yPositions = yLabels.Select(d => YCoordination(ActualHeight, d * Math.Pow(10, power) + minValue, maxValue)).ToArray();
                for (int i = 0; i < yLabels.Count; i++)
                {
                    var yLabel = yLabels[i];
                    var pos = yPositions[i];
                    context.DrawLine(dashPen, new PointD { X = 0, Y = pos }, new PointD { X = ActualWidth, Y = pos });
                    var label = power >= 0 ? ((int)(yLabel * Math.Pow(10, power)) + minValue).ToString() : string.Format($"{{0:F{Math.Abs(power)}}}", yLabel * Math.Pow(10, power) + maxValue);
                    var formattedLabel = new FormattedText(label,
                        CultureInfo.GetCultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface("Verdana"),
                        12, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);
                    context.DrawText(formattedLabel, new PointD { X = 0, Y = pos - formattedLabel.Height });
                }
                #endregion
            }
            context.Close();
            return visual;
        }

        private DrawingVisual DrawPositionLine(PointD p)
        {
            var visual = new DrawingVisual();
            using var context = visual.RenderOpen();
            var ratio = p.X / ActualWidth;
            if (ratio > 0 && ratio < 1)
            {
                var range = Points.Skip(downIndex).Take(displayCount).ToList();
                var point = range[(int)(range.Count * ratio)];
                var xPosition = XCoordination(ActualWidth, ratio * range.Count, range.Count);
                var yPosition = YCoordination(ActualHeight, point.Y, maxValue);
                context.DrawLine(greenDashPen, new PointD { X = xPosition, Y = 0 }, new PointD
                {
                    X = xPosition,
                    Y = ActualHeight
                });
                context.DrawLine(greenDashPen, new PointD { X = 0, Y = yPosition }, new PointD { X = ActualWidth, Y = yPosition });
            }
                context.Close();
                return visual;
        }

        private DrawingVisual DrawLegends(PointD p)
        {
            var visual = new DrawingVisual();
            using var context = visual.RenderOpen();
            var ratio = p.X / ActualWidth;
            if (ratio > 0 && ratio < 1 && maxValue != 0)
            {
                var range = Points.Skip(downIndex).Take(displayCount).ToList();
                var point = range[(int)(range.Count * ratio)];
                var time = Time.ElementAt((int)(range.Count * ratio));
                var xPosition = p.X;
                var yPosition = YCoordination(ActualHeight, point.Y, maxValue);
                var showPoint = new PointD(xPosition, yPosition);
                var label = $"time:{time} -- value:{point.Y}";
                var formattedText = new FormattedText(label,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                12, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);
                var leftTop = showPoint switch
                {
                    var pt when pt.X <= ActualWidth * 0.5 && pt.Y <= ActualHeight * 0.5 => new PointD(pt.X + 2, yPosition - 2),
                    var pt when pt.X > ActualWidth * 0.5 && pt.Y <= ActualHeight * 0.5 => new PointD(pt.X - formattedText.Width - 2, yPosition - 2),
                    var pt when pt.X <= ActualWidth * 0.5 && pt.Y > ActualHeight * 0.5 => new PointD(pt.X + 2, yPosition - formattedText.Height - 2),
                    var pt when pt.X > ActualWidth * 0.5 && pt.Y > ActualHeight * 0.5 => new PointD(pt.X - formattedText.Width - 2, yPosition - formattedText.Height - 2),
                    _ => throw new InvalidOperationException()
                };
                context.DrawRoundedRectangle(Brushes.White, null, new Rect(leftTop, new Size(formattedText.Width + 2, formattedText.Height + 2)), 2, 2);
                context.DrawText(formattedText, new PointD(leftTop.X + 1, leftTop.Y + 1));
            }
            return visual;
        }

        private DrawingVisual DrawBackground()
        {
            var visual = new DrawingVisual();
            using var context = visual.RenderOpen();
            var bankline = new Rect(new Point(0, 0), new Point(ActualWidth, ActualHeight));
            context.DrawRectangle(Brushes.White, null, bankline);
            return visual;
        }
        #endregion

        #region 重写的函数
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            displayCount = Points.Count();
            _children.Clear();
            _children.Add(DrawBackground());
            _children.Add(DrawChart(this, Points, sizeInfo.NewSize.Width, sizeInfo.NewSize.Height));
        }
        protected override int VisualChildrenCount => _children.Count;
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _children[index];
        }
        #endregion
    }
}
