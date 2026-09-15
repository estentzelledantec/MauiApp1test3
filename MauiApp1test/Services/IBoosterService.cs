using AutoMasters.Modeles;
namespace AutoMasters.Services;
public interface IBoosterService
{
    Booster Etat { get; }
    TimeSpan TempsRestant { get; }
    bool PeutOuvrir { get; }
    Task<IReadOnlyList<Carte>> OuvrirAsync();
    void StartTimer();
}
