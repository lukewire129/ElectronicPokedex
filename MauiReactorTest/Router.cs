using MauiReactor;
using MauiReactorTest.Pages;

namespace MauiReactorTest;

internal class Router : Component
{
    public override VisualNode Render() => NavigationPage (new MainPage());
}
