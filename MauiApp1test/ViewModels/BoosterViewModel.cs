using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AutoMasters.Modeles;
using AutoMasters.Services;

namespace AutoMasters.ViewModels;

public partial class BoosterViewModel : BaseViewModel
{
    private readonly IBoosterService _service;
    private readonly IDispatcherTimer _timer;

    public ObservableCollection<Carte> Cartes { get; } = [];

    [ObservableProperty] private int index = -1;
    [ObservableProperty] private string compteur = "";
    [ObservableProperty] private bool fini;

    public Carte? CarteActuelle => Index >= 0 && Index < Cartes.Count ? Cartes[Index] : null;
    public bool HasCurrentCard => CarteActuelle is not null;
    public bool IsReadyToOpen => Cartes.Count == 0 || Fini;

    public BoosterViewModel(IBoosterService service)
    {
        _service = service;
        State = AutoMasters.Modeles.ViewState.Content; // <-- CORRIGÉ ICI

        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += (s, e) => Actualiser();
        _timer.Start();

        Actualiser();
    }

    [RelayCommand]
    public async Task OuvrirAsync()
    {
        if (!_service.PeutOuvrir || State == AutoMasters.Modeles.ViewState.Loading) return; // <-- CORRIGÉ ICI

        try
        {
            State = AutoMasters.Modeles.ViewState.Loading; // <-- CORRIGÉ ICI
            Cartes.Clear();

            await Task.Delay(1500);
            var nouvellesCartes = await _service.OuvrirAsync();

            foreach (var c in nouvellesCartes) Cartes.Add(c);

            Index = 0;
            Fini = false;

            RafraichirProprietes();
            State = AutoMasters.Modeles.ViewState.Content; // <-- CORRIGÉ ICI
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            State = AutoMasters.Modeles.ViewState.Error; // <-- CORRIGÉ ICI
        }
    }

    public void PasserCarteSuivante()
    {
        if (Index < Cartes.Count - 1)
        {
            Index++;
        }
        else
        {
            Fini = true;
            Index = -1;
        }
        RafraichirProprietes();
    }

    private void RafraichirProprietes()
    {
        OnPropertyChanged(nameof(CarteActuelle));
        OnPropertyChanged(nameof(HasCurrentCard));
        OnPropertyChanged(nameof(IsReadyToOpen));
    }

    public void Actualiser()
    {
        _service.Tick();
        Compteur = _service.PeutOuvrir
            ? $"{_service.Etat.Stock}/10 paquets disponibles"
            : $"Prochain dans {_service.TempsRestant:mm\\:ss}";
    }
}