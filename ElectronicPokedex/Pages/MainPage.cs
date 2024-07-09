using MauiReactor;
using MauiReactor.Canvas;
using ElectronicPokedex.Components;

namespace ElectronicPokedex.Pages;


public partial class MainPage : Component
{
    Header header { get; set; }
    public override VisualNode Render()
    {
        header = new Header ();

        return new BaseScreenLayout
        {
            Grid (
                header
                    .GridRow (0),

                GraphicsView ()
                    .OnDraw (Draw)
                    .GridRowSpan (2)
                    .Margin (leftRight: 20, topBottom: 0),

                new HomePage ()
                    .GridRow (1)
            )
            .Rows ("auto, *")
        };
    }
        

    private void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float startHeight = header.GetHeight () + 20;
        PathF path = new PathF ();
        path.MoveTo (1, startHeight);
        path.LineTo (((dirtyRect.Width / 2) - 1) + 11, startHeight);

        float lastHeight = startHeight / 2 + 11; 
        path.LineTo (((dirtyRect.Width / 2) -1) + (dirtyRect.Width / 11) + 11, lastHeight);
        path.LineTo (dirtyRect.Width - 1, lastHeight);
        path.LineTo (dirtyRect.Width - 1, dirtyRect.Height - 20);
        path.LineTo (1, dirtyRect.Height - 20);
        path.Close ();

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 1;
        canvas.DrawPath (path);
    }
}
