using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SoftCareManager.Common.UI.Authorization
{
    internal class RightVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null
                || values.Length < 2
                || values[0] == DependencyProperty.UnsetValue
                || values[1] == DependencyProperty.UnsetValue)
            {
                return Visibility.Collapsed;
            }

            return ShouldBeVisible(values)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static bool ShouldBeVisible(IList<object> values)
        {
            return (Visibility)values[0] == Visibility.Visible && (bool)values[1];
        }
    }
}