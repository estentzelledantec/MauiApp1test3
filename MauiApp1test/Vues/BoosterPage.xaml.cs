using AutoMasters.ViewModels;

namespace AutoMasters.Vues;

public partial class BoosterPage : ContentPage
{
    public BoosterPage(BoosterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnSuivanteClicked(object sender, EventArgs e)
    {
        if (BindingContext is BoosterViewModel vm)
        {
            // 1. Animation de sortie (rétrécit la carte)
            await CardBorder.ScaleTo(0, 150, Easing.CubicIn);

            // 2. On change la donnée dans le ViewModel
            vm.PasserCarteSuivante();

            // 3. Animation d'entrée (agrandit la nouvelle carte)
            if (vm.HasCurrentCard)
            {
                await CardBorder.ScaleTo(1, 150, Easing.CubicOut);
            }
            else
            {
                // Si c'était la dernière carte, on remet l'échelle à 1 discrètement 
                // pour que le paquet soit prêt pour la prochaine ouverture
                CardBorder.Scale = 1;
            }
        }
    }
}