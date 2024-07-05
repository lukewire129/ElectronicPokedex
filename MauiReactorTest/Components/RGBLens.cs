using MauiReactor;

namespace MauiReactorTest.Components
{
    public class RGBLens : Component
    {
        public RGBLens(Color color)
        {
            Color = color;
        }

        public Color Color { get; }

        public override VisualNode Render() =>
            GraphicsView ()
                 .OnDraw (Draw)
                .WidthRequest(30)
                .HeightRequest(30);

        private void Draw(ICanvas canvas, RectF dirtyRect)
        {
            RadialGradientPaint radialGradientPaint = new RadialGradientPaint
            {
                StartColor = Colors.White,
                EndColor = Color
                // Center is already (0.5,0.5)
                // Radius is already 0.5
            };

            RectF radialRectangle = new RectF (0, 13, 25, 0);
            canvas.SetFillPaint (radialGradientPaint, radialRectangle);
            canvas.FillEllipse (3, 3, 25, 25);

            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 1;
            canvas.DrawEllipse (2, 2, 26, 26);
        }
    }
}
