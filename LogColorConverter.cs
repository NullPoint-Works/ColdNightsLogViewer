using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ColdNightsLogViewer;

public class LogColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string line = value as string ?? "";
        
        if (line.Contains("[Error]"))
            return Brushes.Red;
        if (line.Contains("[Warning]"))
            return Brushes.Goldenrod;
        if (line.Contains("[Debug]"))
            return Brushes.RoyalBlue;

        return Brushes.Black;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}