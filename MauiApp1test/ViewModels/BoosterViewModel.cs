using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AutoMasters.Modeles;
using AutoMasters.Services;
namespace AutoMasters.ViewModels;
public partial class BoosterViewModel : BaseViewModel
{
    private readonly IBoosterService _service;
    public ObservableCollection<Carte> Cartes { get; } = [];
    [ObservableProperty] private int index = -1;
    [ObservableProperty] private string compteur = "";
    [ObservableProperty] private bool fini;
    public Carte? CarteActuelle => Index >= 0 && Index < Cartes.Count ? Cartes[Index] : null;
    public bool HasCurrentCard => CarteActuelle is not null;
    public bool IsReadyToOpen => Cartes.Count == 0 || Fini;
    public BoosterViewModel(IBoosterService service) { _service=service; Actualiser(); }
    [RelayCommand] public async Task OuvrirAsync()
    {
        try { Cartes.Clear(); foreach(var c in await _service.OuvrirAsync()) Cartes.Add(c); Index=-1; Fini=false; OnPropertyChanged(nameof(CarteActuelle)); OnPropertyChanged(nameof(HasCurrentCard)); OnPropertyChanged(nameof(IsReadyToOpen)); State=AutoMasters.Modeles.ViewState.Content; Actualiser(); }
        catch(Exception ex) { ErrorMessage=ex.Message; State=AutoMasters.Modeles.ViewState.Error; }
    }
    [RelayCommand] public async Task SuivanteAsync()
    {
        if (Index < Cartes.Count-1) { Index++; OnPropertyChanged(nameof(CarteActuelle)); OnPropertyChanged(nameof(HasCurrentCard)); OnPropertyChanged(nameof(IsReadyToOpen)); }
        else { Fini=true; OnPropertyChanged(nameof(IsReadyToOpen)); OnPropertyChanged(nameof(HasCurrentCard)); }
        await Task.CompletedTask;
    }
    public void Actualiser() { Compteur=$"{_service.Etat.Stock}/10"; }
}
