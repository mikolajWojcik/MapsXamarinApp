﻿using MapsApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MapsApp.Converters
{
    public class EnumToDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Enum enumValue)
            {
                var attribute = enumValue.GetAttribute<EnumHelperAttribute>();
                var pinfo = typeof(EnumHelperAttribute).GetProperty("DisplayName");

                if (attribute != null && pinfo != null)
                    return pinfo.GetValue(attribute);
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
