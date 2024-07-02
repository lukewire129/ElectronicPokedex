using MauiReactor;

namespace MauiReactorTest.Components
{
    internal class NavigationBarComponentState
    {
        public int idx { get; set; }
    }

    internal class NavigationBarComponent : Component<NavigationBarComponentState>
    {
        public override VisualNode Render()
        {
            return ContentView ()
                .HeightRequest(100)
                .Background(Colors.Blue)
                .VEnd ();
        }
    }
}
