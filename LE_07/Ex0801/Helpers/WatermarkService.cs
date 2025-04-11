using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Ex0801.Helpers
{
    public static class WatermarkService
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached(
                "Watermark",
                typeof(string),
                typeof(WatermarkService),
                new FrameworkPropertyMetadata(string.Empty, OnWatermarkChanged));

        public static string GetWatermark(DependencyObject obj) =>
            (string)obj.GetValue(WatermarkProperty);

        public static void SetWatermark(DependencyObject obj, string value) =>
            obj.SetValue(WatermarkProperty, value);

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.Loaded += (s, ev) => AddWatermark(textBox);
                textBox.GotFocus += (s, ev) => UpdateWatermarkVisibility(textBox);
                textBox.LostFocus += (s, ev) => UpdateWatermarkVisibility(textBox);
                textBox.TextChanged += (s, ev) => UpdateWatermarkVisibility(textBox);
            }
        }

        private static void AddWatermark(TextBox textBox)
        {
            var layer = AdornerLayer.GetAdornerLayer(textBox);
            if (layer != null)
            {
                var adorners = layer.GetAdorners(textBox);
                if (adorners == null || Array.Find(adorners, a => a is WatermarkAdorner) == null)
                {
                    layer.Add(new WatermarkAdorner(textBox, GetWatermark(textBox)));
                }
            }
        }

        private static void UpdateWatermarkVisibility(TextBox textBox)
        {
            var layer = AdornerLayer.GetAdornerLayer(textBox);
            if (layer == null) return;

            var adorners = layer.GetAdorners(textBox);
            if (adorners == null) return;

            foreach (var adorner in adorners)
            {
                if (adorner is WatermarkAdorner watermarkAdorner)
                {
                    watermarkAdorner.Visibility = string.IsNullOrEmpty(textBox.Text) && !textBox.IsFocused
                        ? Visibility.Visible
                        : Visibility.Hidden;
                }
            }
        }
    }
}
