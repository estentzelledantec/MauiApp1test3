using System.Globalization;

namespace AutoMasters.Converters;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isSelected = value is bool b && b;
        // Si le bouton est sélectionné, on met un fond rouge accent (#ef4444), sinon un fond sombre neutre (#1e1e1e)
        return isSelected ? Color.FromArgb("#ef4444") : Color.FromArgb("#1e1e1e");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}