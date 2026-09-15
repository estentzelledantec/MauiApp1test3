using AutoMasters.Modeles;
namespace AutoMasters.Services;
public interface IPriceService
{
    decimal CalculerCote(Vehicule vehicule);
    Rarete CalculerRarete(Vehicule vehicule);
}
