using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace SastWiki.WPF.Services
{
    public class WatermarkService : DependencyObject
    {
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
            "Watermark",
            typeof(string),
            typeof(WatermarkService),
            new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsWatermarkEnabledProperty = DependencyProperty.RegisterAttached(
            "IsWatermarkEnabled",
            typeof(bool),
            typeof(WatermarkService),
            new FrameworkPropertyMetadata(false, IsWatermarkEnabledChanged));

        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        public static bool GetIsWatermarkEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsWatermarkEnabledProperty);
        }

        public static void SetIsWatermarkEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsWatermarkEnabledProperty, value);
        }

        private static void IsWatermarkEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox == null)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                textBox.GotFocus += TextBox_GotFocus;
                textBox.LostFocus += TextBox_LostFocus;
                UpdateWatermark(textBox);
            }
            else
            {
                textBox.GotFocus -= TextBox_GotFocus;
                textBox.LostFocus -= TextBox_LostFocus;
                RemoveWatermark(textBox);
            }
        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            RemoveWatermark(textBox);
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            UpdateWatermark(textBox);
        }

        private static void UpdateWatermark(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Tag = textBox.Background;
                textBox.Background = Brushes.Transparent;
                textBox.Text = GetWatermark(textBox);
            }
        }

        private static void RemoveWatermark(TextBox textBox)
        {
            if (textBox.Text == GetWatermark(textBox))
            {
                textBox.Text = string.Empty;
                textBox.Background = (Brush)textBox.Tag;
            }
        }
    }

}
