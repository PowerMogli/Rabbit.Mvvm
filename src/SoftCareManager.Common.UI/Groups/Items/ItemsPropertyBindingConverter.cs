using SoftCareManager.Contracts.Model;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace SoftCareManager.Common.UI.Groups.Items
{
    class ItemsPropertyBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var models = value as ObservableCollection<IModel>;
            return value;
            //return CastToModelInstances(value);
        }

        private static ObservableCollection<IModel> CastToModelInstances(object value)
        {
            IEnumerable items = value as IEnumerable;

            ObservableCollection<IModel> models = new ObservableCollection<IModel>();
            foreach (var item in items)
            {
                models.Add(item as IModel);
            }

            return models;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}