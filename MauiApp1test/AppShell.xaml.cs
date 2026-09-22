namespace AutoMasters;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // On déclare la route de la page Booster pour pouvoir y naviguer, 
        // même si elle n'est pas dans les onglets du bas.
        Routing.RegisterRoute("BoosterPage", typeof(Vues.BoosterPage));
    }
}