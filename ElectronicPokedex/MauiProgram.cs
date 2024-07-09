using CommunityToolkit.Maui;
using MauiReactor;
using ElectronicPokedex.api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace ElectronicPokedex
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder ();
            builder
                .UseMauiReactorApp<Router> (app =>
                {
                    app.AddResource ("Resources/Styles/Colors.xaml");
                    app.AddResource ("Resources/Styles/Styles.xaml");
                })
                .UseMauiCommunityToolkit ()
#if DEBUG
                .EnableMauiReactorHotReload ()
#endif
                .ConfigureFonts (fonts =>
                {
                    fonts.AddFont ("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont ("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                });

#if DEBUG
        		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IPokeMonApi, PokeMonApi> ();

            return builder.Build ();
        }
    }
}
