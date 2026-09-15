namespace AutoMasters.Modeles;
public sealed class Carte
{
    public Guid InstanceId { get; set; } = Guid.NewGuid();
    public Vehicule Vehicule { get; set; } = new();
    public DateTime ObtenueLe { get; set; } = DateTime.UtcNow;
    public decimal PrixAchat { get; set; }
    public bool ALaVente { get; set; }
}
