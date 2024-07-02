using MauiReactor;
using MauiReactor.Canvas;
using MauiReactorTest.api;
using MauiReactorTest.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiReactorTest.Pages;

public class MainPageState
{
    public int Counter { get; set; }
    public ObservableCollection<PokeMonModel> PokeMonModels { get; set; }
    public bool isLoading { get; set; } = true;
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
            foreach (var item in await _api.GetPocketMons (nowindex))
            {
                State.PokeMonModels.Add (item);
            }
            SetState (x => x.isLoading = false);
        });
        base.OnMounted ();
    }
    public override VisualNode Render()
        => ContentPage (
            Grid (               
                CollectionView ()
                    .ItemsSource (State.PokeMonModels, RenderPerson)     
                    .RemainingItemsThreshold(0)
                    .OnRemainingItemsThresholdReached(OnLoaded)
                    .ItemsLayout(new VerticalGridItemsLayout (span: 2))
                    .WidthRequest(220),

                ActivityIndicator ()
                    .IsRunning (State.isLoading)
                    .VCenter ()
                    .HCenter ()
            )
        );

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
                Image (new Uri (pokeMon.imageUrl))
                        .WidthRequest (50)
                        .HeightRequest (50),

                Label (pokeMon.Name)
                    .VCenter ()
                    .HCenter ()
        ))
        .WidthRequest(100)
        .Margin(bottom:10, left:0, right:0, top:0)
            ;
    }
}
