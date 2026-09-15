using AutoMasters.Modeles;
namespace AutoMasters.Services;
public sealed class DemoApiService : IApiService
{
    private readonly List<Vehicule> _vehicules =
    [
        new() { Id=1, Marque="Toyota", Modele="Supra MK4", Motorisation="2JZ-GTE 3.0 Twin Turbo", Annee=1998, PuissanceCh=330, Production=16400, Prix=85000, Rarete=Rarete.Rare, ImageUrl="https://images.unsplash.com/photo-1619767886558-efdc259cde1a?auto=format&fit=crop&w=1000&q=80", Description="Icône japonaise de la génération JDM." },
        new() { Id=2, Marque="Nissan", Modele="Skyline GT-R R34", Motorisation="RB26DETT 2.6 Twin Turbo", Annee=2002, PuissanceCh=280, Production=11378, Prix=220000, Rarete=Rarete.Legendaire, ImageUrl="https://images.unsplash.com/photo-1552519507-da3b142c6e3d?auto=format&fit=crop&w=1000&q=80", Description="L'une des GT japonaises les plus recherchées." },
        new() { Id=3, Marque="Ferrari", Modele="F40", Motorisation="V8 2.9 Twin Turbo", Annee=1990, PuissanceCh=478, Production=1311, Prix=1900000, Rarete=Rarete.Mythique, ImageUrl="https://images.unsplash.com/photo-1592198084033-aade902d1aae?auto=format&fit=crop&w=1000&q=80", Description="Supercar emblématique de Ferrari." },
        new() { Id=4, Marque="BMW", Modele="M3 E46", Motorisation="S54 3.2 I6", Annee=2005, PuissanceCh=343, Production=85000, Prix=55000, Rarete=Rarete.PeuCommune, ImageUrl="https://images.unsplash.com/photo-1555215695-3004980ad54e?auto=format&fit=crop&w=1000&q=80", Description="Référence des sportives compactes des années 2000." },
        new() { Id=5, Marque="Renault", Modele="Clio 2 1.2", Motorisation="1.2 16V", Annee=2004, PuissanceCh=75, Production=900000, Prix=3500, Rarete=Rarete.Commune, ImageUrl="https://images.unsplash.com/photo-1502877338535-766e1452684a?auto=format&fit=crop&w=1000&q=80", Description="Modèle courant et accessible." }
    ];

    public Task<IReadOnlyList<Vehicule>> GetVehiculesAsync(string? recherche=null, Rarete? rarete=null)
    {
        IEnumerable<Vehicule> q = _vehicules;
        if (!string.IsNullOrWhiteSpace(recherche))
            q = q.Where(x => ($"{x.Marque} {x.Modele} {x.Motorisation}").Contains(recherche, StringComparison.OrdinalIgnoreCase));
        if (rarete.HasValue) q = q.Where(x => x.Rarete == rarete);
        return Task.FromResult<IReadOnlyList<Vehicule>>(q.ToList());
    }

    public Task<IReadOnlyList<Enchere>> GetEncheresAsync() => Task.FromResult<IReadOnlyList<Enchere>>([]);
    public IReadOnlyList<Vehicule> All => _vehicules;
}
