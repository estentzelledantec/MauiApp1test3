using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AutoMasters.Services;
namespace AutoMasters.ViewModels;
public partial class AccueilViewModel : BaseViewModel
{
    private readonly IBoosterService _boosters;
    [ObservableProperty] private string stock = "";
    [ObservableProperty] private string prochain = "";
    public AccueilViewModel(IBoosterService boosters) { _boosters=boosters; Refresh(); }
    [RelayCommand] public void Refresh()
    {
        State = AutoMasters.Modeles.ViewState.Content;
        Stock = $"{_boosters.Etat.Stock}/10";
        Prochain = _boosters.TempsRestant == TimeSpan.Zero ? "Disponible" : _boosters.TempsRestant.ToString(@"mm\:ss");
    }
}
