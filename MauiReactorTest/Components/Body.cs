using MauiReactor;
using MauiReactor.Canvas;
using System.Collections.ObjectModel;
using MauiReactorTest.Models;
using MauiReactorTest.api;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace MauiReactorTest.Components;
public class BodyState
{
    public int Counter { get; set; }
    public ObservableCollection<PokeMonModel> PokeMonModels { get; set; }
    public bool isLoading { get; set; } = true;
}
public partial class Body : Component<BodyState>
{
    [Inject]
    IPokeMonApi _api;
    protected override void OnMounted()
    {
        State.PokeMonModels = new ();
        Task.Run (async () =>
        {
            Debug.WriteLine (DateTime.Now);
            foreach (var item in await _api.GetPocketMons ())
            {
                State.PokeMonModels.Add (item);
            }
            Debug.WriteLine (DateTime.Now);
            SetState (x => x.isLoading = false);
        });
        base.OnMounted ();
    }
    private int idx=0;
    public override VisualNode Render() =>
        Grid (
            Grid (
                GraphicsView ()
                    .OnDraw (DisplayDraw),

                HamBuger ()
                    .WidthRequest (30).HeightRequest (20)
                    .VEnd ().HEnd ()
                    .Margin (right: 30, top: 0, bottom: 20, left: 0),

                    Border ()
                        .StrokeCornerRadius (3)
                        .Background (Color.Parse ("#3f373a"))
                        .Margin (30, 32, 30, 45),

                    State.isLoading == true?
                        Image ()
                            .Source ("pikachu.gif")
                            .Margin (0, 40, 0, 50)
                            .IsAnimationPlaying (true)
                            .VCenter ()
                            .HCenter()
                        :
                        Image()
                            .Source (new Uri(State.PokeMonModels[idx].imageUrl))
                            .Margin (0, 40, 0, 50)
                            .IsAnimationPlaying (true)
                            .VCenter ()
                            .HCenter ()
            )
            .HeightRequest (260),

            Grid (
                GraphicsView ()
                    .OnDraw (DetailPokeMonButtonDraw)
                    .WidthRequest (40)
                    .HeightRequest (40)
                    .VStart ()
                    .HStart (),
                Grid (
                    GraphicsView ()
                        .OnDraw (InterfaceDraw)
                        .HeightRequest (200)
                        .WidthRequest (140)
                        .HStart ()
                        .VStart ()
                        .GridColumn (0),

                    GraphicsView ()
                        .OnDraw (ArrowInterfaceDraw)
                        .HeightRequest (140)
                        .WidthRequest (130)
                        .GridColumn (1)
                        .VStart ()
                        .HEnd ()
                )
                .GridColumn (1)
                .ColumnSpacing (10)
                .Columns ("*,auto")
            )
            .ColumnSpacing (20)
            .Columns ("auto, *")
            .GridRow (1)
        )
        .Rows("auto, *")
        .RowSpacing(30)
        .Padding (left: 40, right:40, top: 100, bottom: 70);

    private void DisplayDraw(ICanvas canvas, RectF dirtyRect)
    {
        float strokesize = 4;
        PathF path = GetDisplayPath (dirtyRect, 4);

        canvas.FillColor = Color.Parse ("#dcdded");
        canvas.FillPath (path);

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = strokesize;
        canvas.DrawPath (path);

        Button (canvas, dirtyRect);
    }

    private PathF GetDisplayPath(RectF dirtyRect, float strokesize)
    {
        float width = dirtyRect.Width - strokesize;
        float heigth    = dirtyRect.Height - strokesize;
        PathF path = new PathF ();
        path.MoveTo (strokesize, strokesize);
        path.LineTo (width, strokesize);
        path.LineTo (width, heigth);
        path.LineTo (30, heigth);
        path.LineTo (strokesize, heigth - 30);
        path.Close ();

        return path;
    }

    private void Button(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Color.Parse ("#8d323d");
        canvas.FillEllipse (35, dirtyRect.Height - 35, 15, 15);
        canvas.StrokeSize = 2;
        canvas.DrawEllipse (35, dirtyRect.Height - 35, 15, 15);

        canvas.FillColor = Color.Parse ("#8d323d");
        canvas.FillEllipse ((dirtyRect.Width / 2) - 30, 10, 10, 10);
        canvas.StrokeSize = 1;
        canvas.DrawEllipse ((dirtyRect.Width / 2) - 30, 10, 10, 10);

        canvas.FillColor = Color.Parse ("#8d323d");
        canvas.FillEllipse ((dirtyRect.Width / 2) + 20, 10, 10, 10);
        canvas.DrawEllipse ((dirtyRect.Width / 2) + 20, 10, 10, 10);
    }

    private CanvasView HamBuger() => new CanvasView
    {
        new Column ("6, 6, 6, 6")
        {
            new Path()
                .Data(new PathF()
                        .MoveTo (0, 0)
                        .LineTo(30,0))
                .StrokeSize(2)
                .StrokeColor(Colors.Black),

            new Path()
                .Data(new PathF()
                        .MoveTo (0, 0)
                        .LineTo(30,0))
                .StrokeSize(2)
                .StrokeColor(Colors.Black),

            new Path()
                .Data(new PathF()
                        .MoveTo (0, 0)
                        .LineTo(30,0))
                .StrokeSize(2)
                .StrokeColor(Colors.Black),

            new Path()
                .Data(new PathF()
                        .MoveTo (0, 0)
                        .LineTo(30,0))
                .StrokeSize(2)
                .StrokeColor(Colors.Black),
        }
        .Margin(1)
    };

    private void InterfaceDraw(ICanvas canvas, RectF dirtyRect)
    {
        float width = 55;
        float height = 10;
        canvas.FillColor = Color.Parse ("#934148");
        canvas.FillRoundedRectangle (1, 1, width, height, 10);
        canvas.DrawRoundedRectangle (1, 1, width, height, 10);

        canvas.FillColor = Color.Parse ("#404468");
        canvas.FillRoundedRectangle (80, 1, width, height, 10);
        canvas.DrawRoundedRectangle (80, 1, width, height, 10);


        canvas.FillColor = Color.Parse ("#608172");
        canvas.FillRoundedRectangle (1, 35, dirtyRect.Width - 30, 70, 5);
        canvas.DrawRoundedRectangle (1, 35, dirtyRect.Width - 30, 70, 5);
    }

    private void DetailPokeMonButtonDraw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Color.Parse ("#16333c");
        canvas.FillEllipse (1, 1, 39, 39);
        canvas.DrawEllipse (1, 1, 39, 39);
    }


    private void ArrowInterfaceDraw(ICanvas canvas, RectF dirtyRect)
    {
        float stroke = 3;
        float line = stroke + ((dirtyRect.Width - stroke) / 3);
        PathF path = new PathF ();
        path.MoveTo (stroke, line);
        path.LineTo (line, line);
        path.LineTo (line, stroke);
        path.LineTo (line + line, stroke);
        path.LineTo (line + line, line);
        path.LineTo (line + line + line, line);
        path.LineTo (line + line + line, line + line);
        path.LineTo (line + line, line + line);
        path.LineTo (line + line, line + line + line);
        path.LineTo (line, line + line + line);
        path.LineTo (line, line + line);
        path.LineTo (stroke, line + line);
        path.LineTo (stroke, line);

        canvas.StrokeLineJoin = LineJoin.Round;
        canvas.StrokeLineCap = LineCap.Round;
        canvas.StrokeSize = 5;
        canvas.DrawPath (path);

        canvas.FillColor = Color.Parse ("#233543");
        canvas.FillPath (path);
    }
}
