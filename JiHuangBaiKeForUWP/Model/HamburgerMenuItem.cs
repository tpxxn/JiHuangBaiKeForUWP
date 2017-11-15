using System;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace JiHuangBaiKeForUWP.Model
{
    public class HamburgerMenuItem: INotifyPropertyChanged
    {
        public string Icon { get; set; }
        public string Text { get; set; }
        public Type NavigatePage { get; set; }

        private SolidColorBrush _color = new SolidColorBrush(Colors.White);

        public SolidColorBrush Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        private Visibility _selected = Visibility.Collapsed;
        public Visibility Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
