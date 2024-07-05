using MauiReactor;

namespace MauiReactorTest.Components;

public class Body : Component
{
    public override VisualNode Render() =>
        Grid (
            Grid (
                GraphicsView ()
                    .OnDraw (Draw)
            )
            .HeightRequest(250),

            Grid()
            .Background(Colors.Black)
            .GridRow(1)
        )
        .Rows("auto, *")
        .Margin (left: 50, right:50, top: 70, bottom: 40);

    private void Draw(ICanvas canvas, RectF dirtyRect)
    {
        DisplayBackground (canvas, dirtyRect);
        InterfaceButton (canvas, dirtyRect);
        HamBuger (canvas, dirtyRect);
    }

    private void DisplayBackground(ICanvas canvas, RectF dirtyRect)
    {
        PathF path = GetDisplayPath (dirtyRect);

        canvas.FillColor = Color.Parse ("#dcdded");
        canvas.FillPath (path);

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 4;
        canvas.DrawPath (path);
    }
    private PathF GetDisplayPath(RectF dirtyRect)
    {
        PathF path = new PathF ();
        path.MoveTo (0, 0);
        path.LineTo (dirtyRect.Width, 0);
        path.LineTo (dirtyRect.Width, dirtyRect.Height);
        path.LineTo (30, dirtyRect.Height);
        path.LineTo (0, dirtyRect.Height - 30);
        path.Close ();

        return path;
    }

    private void InterfaceButton(ICanvas canvas, RectF dirtyRect)
    {

        canvas.FillColor = Colors.Red;
        canvas.FillEllipse ((dirtyRect.Width / 2) - 30, 10, 10, 10);

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 1;
        canvas.DrawEllipse ((dirtyRect.Width / 2) - 30, 10, 10, 10);

        canvas.FillColor = Colors.Red;
        canvas.FillEllipse ((dirtyRect.Width / 2) + 20, 10, 10, 10);

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 1;
        canvas.DrawEllipse ((dirtyRect.Width / 2) + 20, 10, 10, 10);

        canvas.FillColor = Colors.Red;
        canvas.FillEllipse (35, dirtyRect.Height - 35, 15, 15);

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.DrawEllipse (35, dirtyRect.Height - 35, 15, 15);

    }

    private void HamBuger(ICanvas canvas, RectF dirtyRect)
    {
        float startX = dirtyRect.Width - 80;
        float startY = dirtyRect.Height - 50;
        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.DrawLine (startX, startY, startX + 40, startY);

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.DrawLine (startX, startY + 10, startX + 40, startY + 10);

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 2;
        canvas.DrawLine (startX, startY + 20, startX + 40, startY + 20);
    }
}
