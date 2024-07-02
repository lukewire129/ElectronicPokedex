using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactorTest.Components;

public class LoadingComponent:Component
{
    public override VisualNode Render() => 
        ContentView (
            VStack(
                ActivityIndicator ()
                .IsRunning(true)
                .HeightRequest(30),
                Label ("Loading")
                    .HCenter ()
                    .VCenter ()
                    .FontSize(30)
            )
            .HCenter()
            .VCenter()
            )
          .HeightRequest (500);
}
