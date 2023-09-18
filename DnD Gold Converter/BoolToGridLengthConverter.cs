using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DnD_Gold_Converter
{
	[ValueConversion(typeof(bool), typeof(GridLength))]
	public class BoolToGridLengthConverter : IValueConverter
	{
		public GridLength VisibleLength { get; set; } = GridLength.Auto;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((bool)value == true) ? VisibleLength : new GridLength(0);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{    // Don't need any convert back
			return DependencyProperty.UnsetValue;
		}
	}
}
