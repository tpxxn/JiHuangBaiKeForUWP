using JiHuangBaiKeForUWP.Model;
using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// Character对话框
    /// </summary>
    public sealed partial class CharacterDialog : Page
    {
        public CharacterDialog(Character c)
        {
            this.InitializeComponent();

            CharacterImage.Source = new BitmapImage(new Uri(c.Picture));
            CharacterName.Text = c.Name;
            CharacterEnName.Text = c.EnName;
            if (c.Motto != null)
            {
                CharacterMotto.Text = c.Motto;
            }
            else
            {
                CharacterMotto.Visibility = Visibility.Collapsed;
            }
            CharacterHunger.Value = c.Hunger;
            CharacterHunger.BarColor = new SolidColorBrush(Colors.Blue);
            CharacterHealth.Value = c.Health;
            CharacterHealth.BarColor = new SolidColorBrush(Colors.Red);
            CharacterSanity.Value = c.Sanity;
            CharacterSanity.BarColor = new SolidColorBrush(Colors.Yellow);
            //            Hunger.Values = new ChartValues<double>(new[] { c.Hunger });
            //            Health.Values = new ChartValues<double>(new[] { c.Health });
            //            Sanity.Values = new ChartValues<double>(new[] { c.Sanity });
            if (c.Name == "海獭伍迪" || c.Name == "阿比盖尔")
            {
                CharacterDamage.Text = $"伤害：{c.Damage} 点";
            }
            else
            {
                CharacterDamage.Text = $"伤害：{c.Damage} 倍";
            }
            if (c.Descriptions != null)
            {
                CharacterDescription1.Text = c.Descriptions[0];
                CharacterDescription1.Visibility = Visibility.Visible;
                if (c.Descriptions.Count >= 2)
                {
                    CharacterDescription2.Text = c.Descriptions[1];
                    CharacterDescription2.Visibility = Visibility.Visible;
                }
                if (c.Descriptions.Count == 3)
                {
                    CharacterDescription3.Text = c.Descriptions[2];
                    CharacterDescription3.Visibility = Visibility.Visible;
                }
            }
            CharacterIntroduction.Text = c.Introduce;
        }
    }
}
