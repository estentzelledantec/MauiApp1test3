using AutoMasters.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace AutoMasters.Vues;

public partial class BoosterPage : ContentPage
{
    public BoosterPage(BoosterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnPrecedentClicked(object sender, EventArgs e)
    {
        var vm = (BoosterViewModel)BindingContext;
        if (vm != null)
        {
            await JouerAnimationLegere(() => vm.PasserCartePrecedente());
        }
    }

    private async void OnSuivantClicked(object sender, EventArgs e)
    {
        var vm = (BoosterViewModel)BindingContext;
        if (vm != null)
        {
            await JouerAnimationLegere(() => vm.PasserCarteSuivante());
        }
    }

    private async void OnTerminerClicked(object sender, EventArgs e)
    {
        var vm = (BoosterViewModel)BindingContext;
        if (vm != null)
        {
            await vm.TerminerOuvertureAsync();
        }
    }

    /// <summary>
    /// Crée une très légère animation de "balayage" (Swipe) sans montrer le dos de la carte.
    /// </summary>
    private async Task JouerAnimationLegere(Action actionChangementDonnees)
    {
        // 1. La carte s'estompe très légèrement (Opacity) et rétrécit un tout petit peu (Scale)
        await Task.WhenAll(
            CarteFace.FadeTo(0.5, 100, Easing.CubicIn),
            CarteFace.ScaleTo(0.95, 100, Easing.CubicIn)
        );

        // 2. Le changement de voiture se fait de manière invisible
        actionChangementDonnees.Invoke();

        // 3. La nouvelle carte revient à la normale (Opacity 1, Scale 1)
        await Task.WhenAll(
            CarteFace.FadeTo(1, 100, Easing.CubicOut),
            CarteFace.ScaleTo(1, 100, Easing.CubicOut)
        );
    }
}