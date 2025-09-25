using System.Globalization;

namespace MauiStart.Converters
{
	public class InverseBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (Boolean.TryParse(value?.ToString(), out bool result))
			{
				return !result;
			}

			return !(value != null);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

	}
}
