using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Project_JanSupierz.View.Converters
{
    public class TowerUpgradeToImageConverter :BloonsConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string id = value.ToString();

            if (UseApi)
            {
                return $"https://statsnite.com/images/btd/towers/{value.ToString()}.png";
            }
            else
            {
                BitmapImage image = null;

                try
                {
                    string path = (DesignerProperties.GetIsInDesignMode(new DependencyObject())) ? $"Resources/Towers/{id}.png" : $"../../Resources/Towers/{id}.png";
                    image = new BitmapImage(new Uri($"pack://application:,,,/{path}", UriKind.Absolute));
                }
                catch
                { 
                    return Binding.DoNothing;
                }

                return image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
