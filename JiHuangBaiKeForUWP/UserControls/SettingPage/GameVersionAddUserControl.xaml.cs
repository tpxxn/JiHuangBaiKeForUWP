using Windows.UI.Xaml.Controls;

namespace JiHuangBaiKeForUWP.UserControls.SettingPage
{
    public sealed partial class GameVersionAddUserControl : UserControl
    {
        public GameVersionAddUserControl()
        {
            this.InitializeComponent();
        }

        public string GetText()
        {
            return InputTextBox.Text;
        }
    }
}
