using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.UserWindows
{
    public class ImageUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Random random= new Random();
            if (value != null && parameter is CollectionViewSource imageUrlListSource)
            {
                IList<string> imageUrlList = imageUrlListSource.View.OfType<string>().ToList();
                string name = value.ToString();
                var imageUrl = imageUrlList.Where(url => url.Contains(name));
                return imageUrl.ToList()[random.Next(0,imageUrl.Count())] != null ? Path.GetFullPath(imageUrl.ToList()[random.Next(0, imageUrl.Count())]) : imageUrl.ToList()[random.Next(0, imageUrl.Count())];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
