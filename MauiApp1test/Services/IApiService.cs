using AutoMasters.Modeles;
namespace AutoMasters.Services;
public interface IApiService
{
    Task<IReadOnlyList<Vehicule>> GetVehiculesAsync(string? recherche = null, Rarete? rarete = null);
    Task<IReadOnlyList<Enchere>> GetEncheresAsync();
}
