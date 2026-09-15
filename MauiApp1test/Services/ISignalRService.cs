namespace AutoMasters.Services;
public interface ISignalRService
{
    bool Connecte { get; }
    Task ConnectAsync();
}
