using RecipeApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace RecipeApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}