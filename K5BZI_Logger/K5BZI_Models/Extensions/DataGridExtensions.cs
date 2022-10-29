﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace K5BZI_Models.Extensions
{
    public class DataGridExtensions : DependencyObject
    {
        public static readonly DependencyProperty SortDescProperty = DependencyProperty.Register(
            "SortDesc", typeof(bool), typeof(DataGridExtensions), new PropertyMetadata(false, OnSortDescChanged));

        private static void OnSortDescChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as DataGrid;

            if (grid != null)
            {
                grid.Sorting += (source, args) =>
                {
                    if (args.Column.SortDirection == null)
                    {
                        // here we check an attached property value of target column
                        var sortDesc = (bool)args.Column.GetValue(DataGridExtensions.SortDescProperty);
                        if (sortDesc)
                        {
                            args.Column.SortDirection = ListSortDirection.Ascending;
                        }
                    }
                };
            }
        }

        public static void SetSortDesc(DependencyObject element, bool value)
        {
            element.SetValue(SortDescProperty, value);
        }

        public static bool GetSortDesc(DependencyObject element)
        {
            return (bool)element.GetValue(SortDescProperty);
        }
    }
}
