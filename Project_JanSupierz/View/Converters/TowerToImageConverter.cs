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
    internal class TowerToImageConverter :BloonsConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;

            string id = value.ToString();

            if (!id.Contains('0'))
            {
                id = $"{id}/tower";
            }

            if (UseApi)
            {
                return $"https://statsnite.com/images/btd/towers/{id}.png";

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
