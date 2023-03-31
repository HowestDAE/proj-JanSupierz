using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Project_JanSupierz.View.Converters
{
    public class IdAndPathToImageConverter : IValueConverter
    {
        private static bool _useApi = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string id = value.ToString();

            if (_useApi)
            {
                return $"https://statsnite.com/images/btd/towers/{value.ToString()}.png";
            }
            else
            {
                BitmapImage image = null;
                

                try
                {
                    image = new BitmapImage(new Uri($"pack://application:,,,/Resources/Towers/{id}.png", UriKind.Absolute));
                }
                catch
                {
                    //Fallback
                    image = new BitmapImage(new Uri($"pack://application:,,,/Resources/Towers/tower.png", UriKind.Absolute));
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
