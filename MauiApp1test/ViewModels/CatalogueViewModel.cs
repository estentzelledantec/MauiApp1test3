using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AutoMasters.Modeles;
using AutoMasters.Services;

namespace AutoMasters.ViewModels;

public partial class CatalogueViewModel : BaseViewModel
{
    private readonly IApiService _api;
    public ObservableCollection<Vehicule> Vehicules { get; } = [];

    [ObservableProperty]
    private string recherche = "";

    [ObservableProperty]
    private Rarete? rareteSelectionnee;

    // On expose la liste des raretés pour le Picker (menu déroulant) XAML
    public List<Rarete?> FiltresRarete { get; } =
    [
        null, // Correspond à "Toutes"
        Rarete.Commune,
        Rarete.PeuCommune,
        Rarete.Rare,
        Rarete.Epique,
        Rarete.Legendaire,
        Rarete.Mythique
    ];

    public CatalogueViewModel(IApiService api)
    {
        _api = api;
        _ = ChargerAsync();
    }

    [RelayCommand]
    public async Task ChargerAsync()
    {
        if (State == AutoMasters.Modeles.ViewState.Loading) return;

        try
        {
            State = AutoMasters.Modeles.ViewState.Loading;
            Vehicules.Clear();

            // Simulation de délai réseau pour voir le loader
            await Task.Delay(500);

            var resultats = await _api.GetVehiculesAsync(Recherche, RareteSelectionnee);
            foreach (var v in resultats)
            {
                Vehicules.Add(v);
            }

            State = Vehicules.Count == 0 ? AutoMasters.Modeles.ViewState.Empty : AutoMasters.Modeles.ViewState.Content;
        }
        catch
        {
            State = AutoMasters.Modeles.ViewState.Offline;
        }

    }
    [RelayCommand]
    public async Task VoirDetailAsync(Vehicule vehiculeSelect)
    {
        if (vehiculeSelect == null) return;

        // On prépare le paramètre à envoyer à l'autre page
        var navigationParameter = new Dictionary<string, object>
        {
            { "VehiculeDetail", vehiculeSelect }
        };

        // Navigation vers la VehiculePage
        await Shell.Current.GoToAsync(nameof(Vues.VehiculePage), navigationParameter);
    }

}