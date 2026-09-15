namespace AutoMasters;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // On enregistre la route de la fiche détaillée pour pouvoir y passer des paramètres
        Routing.RegisterRoute(nameof(Vues.VehiculePage), typeof(Vues.VehiculePage));
    }
}