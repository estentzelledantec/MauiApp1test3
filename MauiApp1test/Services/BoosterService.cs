using AutoMasters.Modeles;
namespace AutoMasters.Services;
public sealed class BoosterService : IBoosterService
{
    private readonly DemoApiService _api = new();
    private readonly Random _random = new();
    public Booster Etat { get; } = new() { Stock = 10, StockMax = 10 };
    public TimeSpan TempsRestant
    {
        get
        {
            if (Etat.ProchainDisponible is null) return TimeSpan.Zero;
            var t = Etat.ProchainDisponible.Value - DateTimeOffset.UtcNow;
            return t > TimeSpan.Zero ? t : TimeSpan.Zero;
        }
    }
    public bool PeutOuvrir => Etat.Stock > 0;
    public Task<IReadOnlyList<Carte>> OuvrirAsync()
    {
        if (!PeutOuvrir) throw new InvalidOperationException("Aucun booster disponible.");
        Etat.Stock--;
        if (Etat.Stock < Etat.StockMax) Etat.ProchainDisponible ??= DateTimeOffset.UtcNow.AddMinutes(10);
        var pool = _api.All;
        var cartes = Enumerable.Range(0,5).Select(_ =>
        {
            var v = pool[_random.Next(pool.Count)];
            return new Carte { Vehicule = v, PrixAchat = v.Prix };
        }).ToList();
        Etat.Cartes = cartes;
        return Task.FromResult<IReadOnlyList<Carte>>(cartes);
    }
    public void StartTimer() { if (Etat.Stock < Etat.StockMax && Etat.ProchainDisponible is null) Etat.ProchainDisponible = DateTimeOffset.UtcNow.AddMinutes(10); }
    public void Tick()
    {
        if (Etat.ProchainDisponible is { } p && p <= DateTimeOffset.UtcNow)
        {
            Etat.Stock = Math.Min(Etat.StockMax, Etat.Stock + 1);
            Etat.ProchainDisponible = Etat.Stock < Etat.StockMax ? p.AddMinutes(10) : null;
        }
    }
}
