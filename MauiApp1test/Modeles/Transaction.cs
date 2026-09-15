namespace AutoMasters.Modeles;
public sealed class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CarteId { get; set; }
    public string Acheteur { get; set; } = "";
    public string Vendeur { get; set; } = "";
    public decimal Montant { get; set; }
    public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
    public Guid IdempotencyKey { get; set; } = Guid.NewGuid();
}
