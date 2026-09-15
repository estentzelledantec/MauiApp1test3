namespace AutoMasters.Services;

public interface ISignalRService
{
    bool Connecte { get; }
    Task ConnectAsync();

    // Ajout : Permet au ViewModel de s'abonner aux changements de prix
    void OnQuoteUpdated(Action<int, decimal> action);
}