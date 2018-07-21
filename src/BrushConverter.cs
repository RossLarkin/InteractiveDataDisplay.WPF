using System;
using System.Windows.Data;
using System.Windows.Media;

namespace InteractiveDataDisplay.WPF
{
    internal class SolidColorBrushToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (!(value is SolidColorBrush solidColorBrush)) {
                throw new InvalidOperationException("Value must be a SolidColorBrush");
            }

            return solidColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (!(value is Brush brush)) {
                throw new InvalidOperationException("Value must be a Brush");
            }

            return (SolidColorBrush)brush;
        }
    }

    internal class BrushToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (!(value is Brush brush)) {
                throw new InvalidOperationException("Value must be a Brush");
            }

            return (SolidColorBrush)brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (!(value is SolidColorBrush solidColorBrush)) {
                throw new InvalidOperationException("Value must be a SolidColorBrush");
            }

            return solidColorBrush;
        }
    }
}
