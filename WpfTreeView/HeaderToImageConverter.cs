using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfTreeView;

/// <summary>
/// Converts a full path to a specific image type of a drive folder or file
/// </summary>
[ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
public class HeaderToImageConverter : IValueConverter
{
    public static HeaderToImageConverter Instance = new HeaderToImageConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var image = (DirectoryItemType)value switch
        {
            DirectoryItemType.Drive => "Images/drive.png",
            DirectoryItemType.Folder => "Images/folder-closed.png",
            _ => "Images/file.png"
        };

        return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
