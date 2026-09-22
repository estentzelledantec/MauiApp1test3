using AutoMasters.ViewModels;

namespace AutoMasters.Vues;

public partial class ConnexionPage : ContentPage
{
    public ConnexionPage(ConnexionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}