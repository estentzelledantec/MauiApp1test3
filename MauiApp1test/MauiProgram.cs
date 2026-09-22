using AutoMasters.Services;
using AutoMasters.ViewModels;
using AutoMasters.Vues;
using Microsoft.Extensions.Logging;

namespace AutoMasters;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        builder.Services.AddSingleton<IApiService, DemoApiService>();
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<IBoosterService, BoosterService>();
        builder.Services.AddSingleton<IPriceService, PriceService>();
        builder.Services.AddSingleton<ISignalRService, SignalRService>();

        builder.Services.AddTransient<AccueilViewModel>();
        builder.Services.AddTransient<CatalogueViewModel>();
        builder.Services.AddTransient<BoosterViewModel>();
        builder.Services.AddTransient<CollectionViewModel>();
        builder.Services.AddTransient<MarcheViewModel>();
        builder.Services.AddTransient<VehiculeViewModel>();
        builder.Services.AddTransient<ConnexionViewModel>();

        builder.Services.AddTransient<AccueilPage>();
        builder.Services.AddTransient<CataloguePage>();
        builder.Services.AddTransient<BoosterPage>();
        builder.Services.AddTransient<CollectionPage>();
        builder.Services.AddTransient<ConnexionPage>();
        builder.Services.AddTransient<MarchePage>();
        builder.Services.AddTransient<VehiculePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
