namespace AutoMasters.Services;
public sealed class SignalRService : ISignalRService
{
    public bool Connecte { get; private set; }
    public Task ConnectAsync() { Connecte = true; return Task.CompletedTask; }
    // Production: HubConnection vers /hubs/market avec JWT et reconnexion automatique.
}
