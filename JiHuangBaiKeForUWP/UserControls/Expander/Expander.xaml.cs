using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace JiHuangBaiKeForUWP.UserControls.Expander
{
    [ContentProperty(Name = "ExpandContent")]
    public sealed partial class Expander : UserControl
    {
        #region 属性：IsExPanded

        public bool IsExPanded
        {
            get => (bool)GetValue(IsExPandedProperty);
            set => SetValue(IsExPandedProperty, value);
        }

        public static readonly DependencyProperty IsExPandedProperty =
            DependencyProperty.Register("IsExPanded", typeof(bool), typeof(Expander), new PropertyMetadata(false, OnIsExPandedChanged));

        private static void OnIsExPandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var expander = (Expander)d;
            if ((bool)e.NewValue)
            {
                expander.ExpandToggleButton.IsChecked = true;
            }
            else
            {
                expander.ExpandToggleButton.IsChecked = false;
            }
        }
        #endregion

        #region 属性：Header

        public object Header
        {
            get => (object)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(Expander), new PropertyMetadata(String.Empty));

        #endregion

        #region 属性：ExpandContent

        public object ExpandContent
        {
            get => (object)GetValue(ExpandContentProperty);
            set => SetValue(ExpandContentProperty, value);
        }

        public static readonly DependencyProperty ExpandContentProperty =
            DependencyProperty.Register("ExpandContent", typeof(object), typeof(Expander), new PropertyMetadata(null));

        #endregion

        public Expander()
        {
            this.InitializeComponent();
            ExpandToggleButton.IsChecked = IsExPanded;
        }

        private void Expand_Button_Click(object sender, RoutedEventArgs e)
        {
            IsExPanded = !IsExPanded;
            if (sender is ToggleButton toggleButton) toggleButton.IsChecked = IsExPanded;
        }
    }
}
