using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace JiHuangBaiKeForUWP.UserControls.Charts
{
    public sealed partial class BarChart : UserControl
    {
        public BarChart()
        {
            this.InitializeComponent();
        }

        #region 依赖属性：标签宽度

        public double LabelWidth
        {
            get => (double)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register("LabelWidth", typeof(double), typeof(BarChart), new PropertyMetadata(false, OnLabelWidthChanged));


        private static void OnLabelWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var barChart = (BarChart)d;

            if ((double)e.NewValue != 0)
            {
                barChart.LabelTextBlock.Width = (double)e.NewValue;
            }
            else
            {
                barChart.LabelTextBlock.Width = 30;
            }
        }
        #endregion

        #region 依赖属性：最大值

        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(BarChart), new PropertyMetadata(1));

        #endregion

        #region 依赖属性：当值为0依然显示
        public bool ShowIfZero
        {
            get => (bool)GetValue(ShowIfZeroProperty);
            set => SetValue(ShowIfZeroProperty, value);
        }

        public static readonly DependencyProperty ShowIfZeroProperty =
            DependencyProperty.Register("Value", typeof(bool), typeof(BarChart), new PropertyMetadata(false, OnShowIfZeroChanged));

        private static void OnShowIfZeroChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var barChart = (BarChart)d;

            if ((bool)e.NewValue)
            {
                barChart.ShowIfZero = true;
            }
            else
            {
                barChart.ShowIfZero = false;
            }
        }
        #endregion

        #region 依赖属性：值

        public double? Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(BarChart), new PropertyMetadata(false, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var barChart = (BarChart)d;
            barChart.ValueTextBlock.Text = e.NewValue.ToString();
            if ((double)e.NewValue != 0 || barChart.ShowIfZero)
            {
                barChart.ValueTextBlock.Text = e.NewValue.ToString();
            }
            else
            {
                barChart.Visibility = Visibility.Collapsed;
            }
            if ((double)e.NewValue < 0)
            {
                barChart.ValueTextBlock.Foreground = new SolidColorBrush(Colors.White);
                barChart.ValueRectangle.Width = -(double)e.NewValue / barChart.MaxValue * 300;
            }
            else
            {
                barChart.ValueRectangle.Width = (double)e.NewValue / barChart.MaxValue * 300;
            }
            if ((double)e.NewValue == 1000)
            {
                barChart.ValueTextBlock.Text = "∞";
            }
        }
        #endregion

        #region 依赖属性：单位

        public string Unit
        {
            get => (string)GetValue(UnitProperty);
            set => SetValue(UnitProperty, value);
        }

        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(BarChart), new PropertyMetadata(false, OnUnitChanged));

        private static void OnUnitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var barChart = (BarChart)d;
            barChart.ValueTextBlock.Text += e.NewValue.ToString();
        }
        #endregion

        #region 依赖属性：标签

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(BarChart), new PropertyMetadata(false, OnLabelChanged));

        private static void OnLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var barChart = (BarChart)d;
            if (string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                barChart.LabelTextBlock.Width = 0;
            }
            if (e.NewValue == null) return;
            barChart.LabelTextBlock.Text = (string)e.NewValue;

        }

        #endregion

        #region 依赖属性：BarChart颜色

        public SolidColorBrush BarColor
        {
            get => (SolidColorBrush)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        public static readonly DependencyProperty BarColorProperty =
            DependencyProperty.Register("BarColor", typeof(SolidColorBrush), typeof(BarChart), new PropertyMetadata(false, OnBarColorChanged));

        private static void OnBarColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var barChart = (BarChart)d;
            barChart.ValueRectangle.Fill = (SolidColorBrush)e.NewValue;
        }

        #endregion

    }
}
