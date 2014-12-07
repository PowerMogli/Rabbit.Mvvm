using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    internal class SelectedItemGroupConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}