using MauiReactor;
using MauiReactorTest.api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace MauiReactorTest
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
