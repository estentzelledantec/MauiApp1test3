using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AutoMasters.Services;

namespace AutoMasters.ViewModels;

public partial class ConnexionViewModel : BaseViewModel
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    private string email = "";

    [ObservableProperty]
    private string motDePasse = "";

    // Gère l'affichage de la zone d'erreur visuelle
    public bool AfficheErreur => !string.IsNullOrWhiteSpace(ErrorMessage);

    public ConnexionViewModel(IAuthService authService)
    {
        _authService = authService;
        State = AutoMasters.Modeles.ViewState.Content;
    }

    [RelayCommand]
    public async Task SeConnecterAsync()
    {
        // 1. Vérification des champs vides
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(MotDePasse))
        {
            ErrorMessage = "Veuillez remplir tous les champs.";
            OnPropertyChanged(nameof(AfficheErreur));
            return;
        }

        // 2. Validation stricte du format de l'email via Regex
        if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            ErrorMessage = "Le format de l'adresse email est invalide.";
            OnPropertyChanged(nameof(AfficheErreur));
            return;
        }

        State = AutoMasters.Modeles.ViewState.Loading;
        ErrorMessage = "";
        OnPropertyChanged(nameof(AfficheErreur));

        // 3. Hachage du mot de passe (SHA-256) avant l'envoi au service
        string motDePasseHashe = HacherMotDePasse(MotDePasse);

        // 4. Authentification avec l'email validé et le mot de passe sécurisé
        bool success = await _authService.LoginAsync(Email, motDePasseHashe);

        if (success)
        {
            // Réinitialisation des champs par sécurité avant de quitter la page
            Email = "";
            MotDePasse = "";

            await Shell.Current.GoToAsync("//MainApp");
        }
        else
        {
            ErrorMessage = "Identifiants incorrects.";
            OnPropertyChanged(nameof(AfficheErreur));
            State = AutoMasters.Modeles.ViewState.Content;
        }
    }

    /// <summary>
    /// Hache le mot de passe en SHA-256 pour ne jamais le faire transiter en clair.
    /// </summary>
    private static string HacherMotDePasse(string motDePasseClair)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(motDePasseClair);
        byte[] hash = sha256.ComputeHash(bytes);

        // Convertit le tableau d'octets en chaîne hexadécimale lisible
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}