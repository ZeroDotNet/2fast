﻿using System;
using Windows.UI.Xaml.Data;

namespace Project2FA.UWP.Converters
{
    public class FavouriteTooltipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {
                return Strings.Resources.AccountCodePageTooltipDeleteFavourite;
            }
            else
            {
                return Strings.Resources.AccountCodePageTooltipSetFavourite;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
