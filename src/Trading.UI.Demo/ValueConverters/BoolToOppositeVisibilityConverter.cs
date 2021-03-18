﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Trading.UI.Demo.ValueConverters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToOppositeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result = Visibility.Collapsed;

            if (value != null)
            {
                bool valueBool = (bool)value;

                result = valueBool ? Visibility.Collapsed : Visibility.Visible;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}