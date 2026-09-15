# AUTOMASTERS — version MAUI multi-plateforme propre

Cette version reprend la structure du projet MAUI de référence : multi-targeting Android/iOS/MacCatalyst/Windows, dossier `Modeles`, dossier `Vues`, `Platforms`, `Resources`, `App.xaml`, `AppShell.xaml` et `MauiProgram.cs`.

Corrections incluses :
- `Platforms/Windows/App.xaml` utilise `maui:MauiWinUIApplication`.
- `Platforms/Windows/Program.cs` supprimé : le modèle MAUI de référence n'en utilise pas.
- `Platforms/Windows/App.xaml.cs` contient `InitializeComponent()`.
- `CollectionView Padding="15"` corrigé dans `Vues/MarchePage.xaml`.
- styles déplacés dans `Resources/Styles`.
- anciennes références `Models`/`Views` alignées sur `Modeles`/`Vues`.
- aucun fichier `bin`/`obj` inclus.
- les références de polices supprimées tant que les fichiers de police ne sont pas présents.

Après extraction : fermer Visual Studio, supprimer `bin` et `obj` s'ils existent, puis ouvrir `MauiApp1test.slnx`, restaurer NuGet et générer la solution.
