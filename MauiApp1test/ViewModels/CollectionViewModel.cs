using System.Collections.ObjectModel;
using AutoMasters.Modeles;
namespace AutoMasters.ViewModels;
public sealed class CollectionViewModel : BaseViewModel
{
    public ObservableCollection<Carte> Cartes { get; } = [];
    public decimal ValeurTotale => Cartes.Sum(x => x.Vehicule.Prix);
    public CollectionViewModel()
    {
        State=AutoMasters.Modeles.ViewState.Content;
        Cartes.Add(new Carte { Vehicule=new Vehicule { Marque="Ferrari", Modele="F40", Motorisation="V8 2.9 Twin Turbo", Prix=1900000, Rarete=Rarete.Mythique, ImageUrl="https://images.unsplash.com/photo-1592198084033-aade902d1aae?auto=format&fit=crop&w=1000&q=80" }});
    }
}
