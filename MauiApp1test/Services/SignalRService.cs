using Microsoft.AspNetCore.SignalR.Client;

namespace AutoMasters.Services;

public sealed class SignalRService : ISignalRService
{
    private Action<int, decimal>? _onQuoteAction;
    public bool Connecte { get; private set; }

    public async Task ConnectAsync()
    {
        if (Connecte) return;
        Connecte = true;

        // SIMULATEUR DE MARCHÉ : En attendant le vrai serveur SignalR
        _ = Task.Run(async () =>
        {
            var rnd = new Random();
            while (true)
            {
                await Task.Delay(2000); // Le marché bouge toutes les 2 secondes

                int randomVehiculeId = rnd.Next(1, 6); // On a 5 véhicules (ID 1 à 5)
                decimal variation = rnd.Next(-500, 500); // Le prix varie entre -500€ et +500€

                // On renvoie la nouvelle donnée sur le thread principal de l'interface
                Application.Current?.Dispatcher.Dispatch(() =>
                {
                    _onQuoteAction?.Invoke(randomVehiculeId, variation);
                });
            }
        });

        await Task.CompletedTask;
    }

    public void OnQuoteUpdated(Action<int, decimal> action)
    {
        _onQuoteAction = action;
    }
}