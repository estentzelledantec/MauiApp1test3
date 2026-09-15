using AutoMasters.ViewModels;
namespace AutoMasters.Vues;
public partial class CollectionPage : ContentPage
{
    public CollectionPage(CollectionViewModel vm) { InitializeComponent(); BindingContext=vm; }
}
