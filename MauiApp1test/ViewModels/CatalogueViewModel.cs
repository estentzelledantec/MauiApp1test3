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
    [ObservableProperty] private string recherche = "";
    [ObservableProperty] private Rarete? rarete;
    public CatalogueViewModel(IApiService api) { _api=api; _=ChargerAsync(); }
    [RelayCommand] public async Task ChargerAsync()
    {
        try { State=AutoMasters.Modeles.ViewState.Loading; Vehicules.Clear(); foreach(var v in await _api.GetVehiculesAsync(Recherche, Rarete)) Vehicules.Add(v); State=Vehicules.Count==0?AutoMasters.Modeles.ViewState.Empty:AutoMasters.Modeles.ViewState.Content; }
        catch { State=AutoMasters.Modeles.ViewState.Offline; }
    }
}
