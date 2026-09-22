using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using AutoMasters.Modeles;

namespace AutoMasters.ViewModels;

public partial class CollectionViewModel : BaseViewModel
{
    public ObservableCollection<Carte> Cartes { get; } = [];

    // Calculs globaux du portefeuille (Exigences EF051, EF052, EF053)
    public decimal ValeurTotale => Cartes.Sum(c => c.Vehicule.Prix);
    public decimal CoutTotal => Cartes.Sum(c => c.PrixAchat);
    public decimal PlusValue => ValeurTotale - CoutTotal;

    // Évite la division par zéro si le portefeuille est vide
    public decimal PlusValuePourcentage => CoutTotal > 0 ? (PlusValue / CoutTotal) * 100 : 0;

    // Booléen pratique pour colorer le texte en vert ou rouge dans le XAML
    public bool EstEnProfit => PlusValue >= 0;

    public CollectionViewModel()
    {
        State = AutoMasters.Modeles.ViewState.Content;

        // Fausse données pour tester le calcul des gains et des pertes
        Cartes.Add(new Carte
        {
            Vehicule = new Vehicule { Marque = "Ferrari", Modele = "F40", Prix = 2100000, ImageUrl = "https://images.unsplash.com/photo-1592198084033-aade902d1aae?auto=format&fit=crop&w=1000&q=80" },
            PrixAchat = 1900000 // Achetée 1.9M, vaut 2.1M (Gain)
        });

        Cartes.Add(new Carte
        {
            Vehicule = new Vehicule { Marque = "Renault", Modele = "Clio 2", Prix = 3000, ImageUrl = "https://images.unsplash.com/photo-1502877338535-766e1452684a?auto=format&fit=crop&w=1000&q=80" },
            PrixAchat = 3500 // Achetée 3.5k, vaut 3k (Perte)
        });
    }
}