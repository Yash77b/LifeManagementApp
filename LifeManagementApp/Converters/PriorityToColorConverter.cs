using System.Globalization;

namespace LifeManagementApp.Converters
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int priority)
            {
                if (priority >= 4)
                    return Colors.Red;
                if (priority == 3)
                    return Colors.Orange;
            }
            return Colors.Green;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}