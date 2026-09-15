using AutoMasters.Modeles;
namespace AutoMasters.Services;
public sealed class PriceService : IPriceService
{
    public decimal CalculerCote(Vehicule v) =>
        Math.Max(500, Math.Round(v.Prix * (1 + (v.Production < 5000 ? .18m : 0) + (DateTime.Now.Year-v.Annee > 30 ? .12m : 0)), 0));
    public Rarete CalculerRarete(Vehicule v)
    {
        if (v.Prix >= 1000000 && v.Production < 3000) return Rarete.Mythique;
        if (v.Prix >= 150000 && v.Production < 20000) return Rarete.Legendaire;
        if (v.Prix >= 50000 && v.Production < 100000) return Rarete.Rare;
        if (v.Prix >= 10000) return Rarete.PeuCommune;
        return Rarete.Commune;
    }
}
