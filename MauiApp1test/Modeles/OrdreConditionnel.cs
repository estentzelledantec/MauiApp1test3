namespace AutoMasters.Modeles;
public sealed class OrdreConditionnel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int VehiculeId { get; set; }
    public decimal Seuil { get; set; }
    public bool Achat { get; set; }
    public bool Actif { get; set; } = true;
}
