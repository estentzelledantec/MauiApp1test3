using AutoMasters.ViewModels;
namespace AutoMasters.Vues;
public partial class CataloguePage : ContentPage
{
    public CataloguePage(CatalogueViewModel vm) { InitializeComponent(); BindingContext=vm; }
}
