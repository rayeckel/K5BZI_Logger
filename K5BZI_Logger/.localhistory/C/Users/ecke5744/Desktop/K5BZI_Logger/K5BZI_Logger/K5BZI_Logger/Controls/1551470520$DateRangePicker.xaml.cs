using System;
using System.Windows;
using System.Windows.Controls;

namespace TMTool.Controls
{
    public partial class DateRangePicker : UserControl
    {
        public event EventHandler FromDateChanged;
        public event EventHandler ToDateChanged;

        public static readonly DependencyProperty DateFromProperty =
                DependencyProperty.Register( "DateFrom", typeof( DateTime? ), typeof( DateRangePicker ),
                                    new PropertyMetadata( DateFrom_PropertyChanged ) );

        public static readonly DependencyProperty DateToProperty =
                 DependencyProperty.Register( "DateTo", typeof( DateTime? ), typeof( DateRangePicker ),
                                     new PropertyMetadata( DateTo_PropertyChanged ) );

        public DateTime? DateFrom
        {
            get { return ( DateTime? ) this.GetValue( DateFromProperty ); }
            set
            {
                SetValue( DateFromProperty, value );
            }
        }

        public DateTime? DateTo
        {
            get { return ( DateTime? ) this.GetValue( DateToProperty ); }
            set
            {
                SetValue( DateToProperty, value );
            }
        }

        public DateRangePicker()
        {
            InitializeComponent();
        }

        private static void DateFrom_PropertyChanged( DependencyObject obj, DependencyPropertyChangedEventArgs e )
        {
            var dateRangePicker = ( DateRangePicker ) obj;
            dateRangePicker.DateFrom = ( DateTime? ) e.NewValue;
            dateRangePicker.DateFromChanged();
        }

        private void DateFromChanged()
        {
            DatePickerTo.BlackoutDates.Clear();

            if ( DateFrom.HasValue )
            {
                var dateFrom = this.DateFrom.Value;
                if ( DateTo.HasValue )
                {
                    var dateTo = this.DateTo.Value;
                    if ( dateTo <= dateFrom )
                    {
                        DateTo = null;
                        OnDateChanged( ToDateChanged );
                    }
                }

                DatePickerTo.BlackoutDates.Add( new CalendarDateRange( DateTime.MinValue, dateFrom.AddDays( -1 ) ) );
            }

            OnDateChanged( FromDateChanged );
        }

        private static void DateTo_PropertyChanged( DependencyObject obj, DependencyPropertyChangedEventArgs e )
        {
            var dateRangePicker = ( DateRangePicker ) obj;
            dateRangePicker.DateTo = ( DateTime? ) e.NewValue;
            dateRangePicker.OnDateChanged( dateRangePicker.ToDateChanged );
        }

        private void OnDateChanged( EventHandler handler )
        {
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
