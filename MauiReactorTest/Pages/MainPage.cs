using MauiReactor;
using MauiReactor.Canvas;
using MauiReactorTest.api;
using MauiReactorTest.Components;
using MauiReactorTest.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiReactorTest.Pages;

public class MainPageState
{
    public int Counter { get; set; }
    public ObservableCollection<PokeMonModel> PokeMonModels { get; set; }
    public bool isLoading { get; set; } = false;
}

public partial class MainPage : Component<MainPageState>
{
    [Inject]
    IPokeMonApi _api;
    private int nowindex = 0;
    protected override void OnMounted()
    {
        State.PokeMonModels = new ();
        Task.Run (async () =>
        {
            //foreach (var item in await _api.GetPocketMons (nowindex))
            //{
            //    State.PokeMonModels.Add (item);
            //}
            //SetState (x => x.isLoading = false);
        });
        base.OnMounted ();
    }
    Header header { get; set; }
    public override VisualNode Render()
    {
        header = new Header ();
        return ContentPage (
            Grid (
                header
                    .GridRow (0),
                new Body ()
                    .GridRow (1),

                GraphicsView ()
                    .OnDraw (Draw)
                    .GridRowSpan (2)
                    .Margin(leftRight:20, topBottom: 0)
            )
            .Rows ("auto, *")
            .Background (Color.Parse ("#ca2137"))
        ).HasNavigationBar(false);
    }
        

    private void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float startHeight = header.GetHeight () + 20;
        PathF path = new PathF ();
        path.MoveTo (0, startHeight);
        path.LineTo (dirtyRect.Width / 2 + 11, startHeight);

        float lastHeight = startHeight / 2 + 11; 
        path.LineTo (dirtyRect.Width / 2 + (dirtyRect.Width / 11) + 11, lastHeight);
        path.LineTo (dirtyRect.Width, lastHeight);
        path.LineTo (dirtyRect.Width, dirtyRect.Height - 20);
        path.LineTo (0, dirtyRect.Height - 20);
        path.Close ();

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 1;
        canvas.DrawPath (path);
    }

    private void OnLoaded()
    {
        if (State.isLoading == true)
        {
            return;
        }
        nowindex++;
        SetState (x => x.isLoading = true);
        Task.Run (async () =>
        {
             foreach (var item in await _api.GetPocketMons (nowindex))
            {
                State.PokeMonModels.Add (item);
            }
            SetState (x => x.isLoading = false);
        });
    }
    private VisualNode RenderPerson(PokeMonModel pokeMon)
    {
        return Frame (
            VStack (
                Label($"#{pokeMon.idx}"),
                Image (new Uri (pokeMon.imageUrl))
                        .WidthRequest (40)
                        .HeightRequest (40)
                        .IsAnimationPlaying(true),

                Label (pokeMon.Name)
                    .VCenter ()
                    .HCenter ()
        ))
        .WidthRequest(100)
        .Margin(bottom:10, left:0, right:0, top:0)
            ;
    }
}
