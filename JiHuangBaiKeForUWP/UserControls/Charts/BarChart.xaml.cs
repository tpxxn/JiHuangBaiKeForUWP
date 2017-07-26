using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace JiHuangBaiKeForUWP.UserControls.Charts
{
    public sealed partial class BarChart : UserControl
    {
        public BarChart()
        {
            this.InitializeComponent();
        }

        #region 依赖属性：值

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(BarChart), new PropertyMetadata(false, OnValueChang));

        private static void OnValueChang(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var barChart = (BarChart)d;
                if ((double)e.NewValue != 0)
                {
                    barChart.ValueTextBlock.Text = e.NewValue.ToString();
                }
                else
                {
                    barChart.Visibility = Visibility.Collapsed;
                }
                barChart.ValueRectangle.Width = (double)e.NewValue / 2;
            }
        }
        #endregion

        #region 依赖属性：标签

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(BarChart), new PropertyMetadata(false, OnLabelChang));

        private static void OnLabelChang(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var barChart = (BarChart)d;
                barChart.LabelTextBlock.Text = (string)e.NewValue;
            }
        }

        #endregion

        #region 依赖属性：BarChart颜色

        public SolidColorBrush BarColor
        {
            get { return (SolidColorBrush)GetValue(BarColorProperty); }
            set { SetValue(BarColorProperty, value); }
        }

        public static readonly DependencyProperty BarColorProperty =
            DependencyProperty.Register("BarColor", typeof(SolidColorBrush), typeof(BarChart), new PropertyMetadata(false, OnBarColorChang));

        private static void OnBarColorChang(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var barChart = (BarChart)d;
                barChart.ValueRectangle.Fill = (SolidColorBrush)e.NewValue;
            }
        }

        #endregion

    }
}
