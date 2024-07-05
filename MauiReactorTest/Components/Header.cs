using MauiReactor;
using MauiReactor.Shapes;

namespace MauiReactorTest.Components
{
    public class Header : Component
    {
        private float _height;
        public float GetHeight() => _height;
        public override VisualNode Render() =>
            Grid (
                    HStack (
                        Lens ()
                            .HStart (),
                        HStack (
                            new RGBLens (Color.Parse ("#a42232")),
                            new RGBLens (Color.Parse ("#947b02")),
                            new RGBLens (Color.Parse ("#005537"))
                        )
                        .VStart ()
                        .Spacing (15)
                    )
                    .Padding (leftRight: 10, topBottom: 5)
                    .Spacing (20),

                    GraphicsView ()
                        .OnDraw (Draw)
                );
        private void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _height = dirtyRect.Height;
            PathF path = new PathF ();
            path.MoveTo (0, dirtyRect.Height);
            path.LineTo (dirtyRect.Width / 2, dirtyRect.Height);
            path.LineTo (dirtyRect.Width / 2 + (dirtyRect.Width / 12), dirtyRect.Height / 2);
            path.LineTo (dirtyRect.Width, dirtyRect.Height / 2);
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 1;
            canvas.DrawPath (path);
        }

        private Grid Lens() =>
              Grid (
                    Ellipse (
                        )
                        .WidthRequest (80)
                        .HeightRequest (80)
                        .Background (Colors.White)
                        .StrokeThickness (1)
                        .Stroke (Colors.Black),
                    Ellipse ()
                            .WidthRequest (60)
                            .HeightRequest (60)
                            .Background (Color.Parse ("#299cd5"))
                            .StrokeThickness (1)
                            .Stroke (Colors.Black),
                    Grid (
                        Ellipse ()
                                .WidthRequest (25)
                                .HeightRequest (25)
                                .Background (Color.Parse ("#94d0f4"))
                                .HStart ()
                                .VStart ()
                        ).Padding (20)

                );
    }
}
