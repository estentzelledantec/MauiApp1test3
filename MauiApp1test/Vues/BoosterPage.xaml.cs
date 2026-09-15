using AutoMasters.ViewModels;
namespace AutoMasters.Vues;
public partial class BoosterPage : ContentPage
{
    public BoosterPage(BoosterViewModel vm) { InitializeComponent(); BindingContext=vm; }
}
