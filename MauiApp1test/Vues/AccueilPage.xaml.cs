using AutoMasters.ViewModels;
namespace AutoMasters.Vues;
public partial class AccueilPage : ContentPage
{
    public AccueilPage(AccueilViewModel vm) { InitializeComponent(); BindingContext=vm; }
}
