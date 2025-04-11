using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Ex0801.Helpers
{
    public class WatermarkAdorner : Adorner
    {
        private readonly TextBlock _textBlock;

        public WatermarkAdorner(UIElement adornedElement, string watermarkText)
            : base(adornedElement)
        {
            IsHitTestVisible = false;

            _textBlock = new TextBlock
            {
                Text = watermarkText,
                Foreground = Brushes.Gray,
                Margin = new Thickness(5, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center
            };

            AddVisualChild(_textBlock);
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index) => _textBlock;

        protected override Size MeasureOverride(Size constraint)
        {
            if (double.IsInfinity(constraint.Width) || double.IsInfinity(constraint.Height))
            {
                constraint = new Size(200, 30); 
            }

            _textBlock.Measure(constraint);

            Size desiredSize = _textBlock.DesiredSize;
            if (double.IsInfinity(desiredSize.Width) || double.IsInfinity(desiredSize.Height))
            {
                desiredSize = new Size(200, 30);
            }

            return desiredSize;
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            _textBlock.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}
