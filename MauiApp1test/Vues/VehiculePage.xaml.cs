using AutoMasters.ViewModels;
namespace AutoMasters.Vues;
public partial class VehiculePage : ContentPage
{
    public VehiculePage(VehiculeViewModel vm) { InitializeComponent(); BindingContext=vm; }
}
