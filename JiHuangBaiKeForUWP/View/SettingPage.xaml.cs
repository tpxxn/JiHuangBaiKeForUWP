using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
       
        #region 构造器

        public SettingPage()
        {
            this.InitializeComponent();
            RootFrame.SourcePageType = typeof(SettingChildPage.SettingChildPage);
        }
        #endregion

        private void ListBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var listBoxItem = (ListBoxItem)((ListBox)sender).SelectedItem;
            if (listBoxItem != null)
            {
                var listBoxItemName = listBoxItem.Name;

                switch (listBoxItemName)
                {
                    case "SettingBoxItem":
                        RootFrame.Navigate(typeof(SettingChildPage.SettingChildPage));
                        break;
                    case "CortanaBoxItem":
                        RootFrame.Navigate(typeof(SettingChildPage.CortanaChildPage));
                        break;
                    case "FeedbackBoxItem":
                        RootFrame.Navigate(typeof(SettingChildPage.FeedbackChildPage));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
