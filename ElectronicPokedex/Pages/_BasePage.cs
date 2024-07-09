using CommunityToolkit.Maui.Core;
using MauiReactor;
using System;

namespace ElectronicPokedex.Pages;
[Scaffold (typeof (CommunityToolkit.Maui.Behaviors.StatusBarBehavior))]
partial class StatusBarBehavior { }

partial class BaseScreenLayout : Component
{
    [Prop]
    Color _statusBarColor = Color.Parse ("#a44d57");

    [Prop]
    Action? _onAppearing;

    public override VisualNode Render()
         => ContentPage ([..
                Children(),
                #if !IOS
                    new StatusBarBehavior()
                        .StatusBarColor(_statusBarColor)
                        .StatusBarStyle(StatusBarStyle.LightContent)
#endif
                    ])
        .Padding (-1)
        .BackgroundColor (_statusBarColor)
        .HasNavigationBar (false)
        .OnAppearing (_onAppearing)
        ;
}
