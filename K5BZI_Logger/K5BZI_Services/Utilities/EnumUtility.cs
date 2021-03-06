﻿using K5BZI_Services.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace K5BZI_Services.Utilities
{
    public class EnumUtility : IValueConverter, IEnumUtility
    {
        public string GetEnumDescription(Enum enumObj)
        {
            var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
            var attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                var attrib = attribArray[0] as DescriptionAttribute;

                return attrib.Description;
            }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var myEnum = (Enum)value;
            string description = GetEnumDescription(myEnum);

            return description;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}