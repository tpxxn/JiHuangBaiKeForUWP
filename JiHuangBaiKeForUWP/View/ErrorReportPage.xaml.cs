using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// 《饥荒百科全书 by tpxxn》应用错误报告页面
    /// </summary>
    public sealed partial class ErrorReportPage : Page
    {
        private bool _includeDeviceInfo;

        public ErrorReportPage(string errorStack)
        {
            this.InitializeComponent();
            if (Global.RootFrame.Content != null) ViewNameTextBox.Text = Global.RootFrame.Content.ToString();
            ErrorStackTextBox.Text = errorStack;
        }

        private async void SubmitButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            _includeDeviceInfo = DeviceInfoCheckBox.IsChecked == true;
            await ReportError(ViewNameTextBox.Text, ErrorStackTextBox.Text, _includeDeviceInfo);
        }

        private async void FeedbackButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }

        private async void GithubButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/tpxxn/JiHuangBaiKeForUWP/issues"));
        }

        private async void QqButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://shang.qq.com/wpa/qunwpa?idkey=c7bd1fac7312bb1afbfde97bec4095e68465b04dc1b262759518cbb876a3bae1"));
        }

        public static async Task ReportError(string viewName, string errorStack = null, bool includeDeviceInfo = true)
        {
            var deviceInfo = new EasClientDeviceInformation();

            const string to = "351765204@qq.com";
            const string subject = "《饥荒百科全书 by tpxxn》应用错误报告";
            var body = $"错误堆栈：{errorStack}  " +
                       $"(程序版本：{GetAppVersion()}, " +
                       $"所在页面：{viewName}, ";

            if (includeDeviceInfo)
            {
                body += $", 设备名：{deviceInfo.FriendlyName}, " +
                        $"操作系统：{deviceInfo.OperatingSystem}, " +
                        $"SKU：{deviceInfo.SystemSku}, " +
                        $"产品名称：{deviceInfo.SystemProductName}, " +
                        $"制造商：{deviceInfo.SystemManufacturer}, " +
                        $"固件版本：{deviceInfo.SystemFirmwareVersion}, " +
                        $"硬件版本：{deviceInfo.SystemHardwareVersion})";
            }
            else
            {
                body += ")";
            }

            await CallExternalContent.OpenEmailComposeAsync(to, subject, body);
        }

        public static string GetAppVersion()
        {
            var ver = Package.Current.Id.Version;
            return $"{ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}";
        }

    }
}
