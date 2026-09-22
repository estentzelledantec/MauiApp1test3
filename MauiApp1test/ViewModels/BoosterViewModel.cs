using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AutoMasters.Modeles;
using AutoMasters.Services;

namespace AutoMasters.ViewModels;

[QueryProperty(nameof(AutoOuverture), "AutoOuverture")]
public partial class BoosterViewModel : BaseViewModel
{
    private readonly IBoosterService _service;
    private readonly IDispatcherTimer _timer;

    public ObservableCollection<Carte> Cartes { get; } = [];

    [ObservableProperty] private int index = -1;
    [ObservableProperty] private string compteur = "";
    [ObservableProperty] private bool fini;

    [ObservableProperty] private bool autoOuverture;

    public Carte? CarteActuelle => Index >= 0 && Index < Cartes.Count ? Cartes[Index] : null;
    public bool HasCurrentCard => CarteActuelle is not null;
    public bool IsReadyToOpen => Cartes.Count == 0 || Fini;

    public BoosterViewModel(IBoosterService service)
    {
        _service = service;
        State = AutoMasters.Modeles.ViewState.Content;

        _timer = Application.Current.Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += (s, e) => Actualiser();
        _timer.Start();

        Actualiser();
    }

    partial void OnAutoOuvertureChanged(bool value)
    {
        if (value)
        {
            AutoOuverture = false;

            if (IsReadyToOpen && _service.PeutOuvrir)
            {
                _ = OuvrirAsync();
            }
        }
    }

    [RelayCommand]
    public async Task OuvrirAsync()
    {
        if (!_service.PeutOuvrir || State == AutoMasters.Modeles.ViewState.Loading) return;

        try
        {
            State = AutoMasters.Modeles.ViewState.Loading;

            // 1. Sécurité : on vide visuellement l'ancienne carte de l'écran avant de charger
            Index = -1;
            RafraichirProprietes();
            Cartes.Clear();

            await Task.Delay(1500); // Faux temps de chargement
            var nouvellesCartes = await _service.OuvrirAsync();

            // 2. Astuce Gacha : On mélange l'ordre des cartes pour que chaque tirage paraisse unique
            var cartesMelangees = nouvellesCartes.OrderBy(x => Guid.NewGuid()).ToList();

            foreach (var c in cartesMelangees) Cartes.Add(c);

            Index = 0;
            Fini = false;

            RafraichirProprietes();
            State = AutoMasters.Modeles.ViewState.Content;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            State = AutoMasters.Modeles.ViewState.Error;
        }
    }

    public async void PasserCarteSuivante()
    {
        if (Index < Cartes.Count - 1)
        {
            Index++;
            RafraichirProprietes();
        }
        else
        {
            // Fin du paquet
            Fini = true;
            Index = -1;
            Cartes.Clear(); // On nettoie tout derrière nous pour la prochaine fois
            RafraichirProprietes();

            // 3. Boucle parfaite : on ramène l'utilisateur sur ton beau menu d'accueil !
            await Shell.Current.GoToAsync("//AccueilPage");
        }
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