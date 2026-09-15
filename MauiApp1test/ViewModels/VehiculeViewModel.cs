using CommunityToolkit.Mvvm.ComponentModel;
using AutoMasters.Modeles;
namespace AutoMasters.ViewModels;
public partial class VehiculeViewModel : BaseViewModel
{
    [ObservableProperty] private Vehicule? vehicule;
    public void Load(Vehicule v) { Vehicule=v; State=AutoMasters.Modeles.ViewState.Content; }
}
