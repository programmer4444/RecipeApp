using Newtonsoft.Json;
using RecipeApp.Models;
using RecipeApp.ViewModels;
using RecipeApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        private readonly HttpClient _httpClient;
        public ItemsPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();


        }
        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            // Get the search query entered by the user
            string query = searchEntry.Text;

            // Check if the search query is not empty or whitespace
            if (!string.IsNullOrWhiteSpace(query))
            {
                // Perform the search operation using the provided query
                string apiUrl = $"https://recipeapp97.azurewebsites.net/recipe/search?query={Uri.EscapeDataString(query)}";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var recipes = JsonConvert.DeserializeObject<List<Recipes>>(content);

                    if (recipes != null && recipes.Any())
                    {
                        recipesCollectionView.ItemsSource = recipes;
                    }
                    else
                    {
                        await DisplayAlert("No Recipes", "No recipes found for the search query", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Failed to search for recipes", "OK");
                }
            }
            else
            {
                // Display an error message indicating that the search query is empty
                await DisplayAlert("Error", "Please enter a search query", "OK");
            }
        }
        private async void OnImageTapped(object sender, EventArgs e)
        {
            // Handle the image tap event here
            // You can access the tapped image data or perform any desired action
            if (sender is Image tappedImage && tappedImage.BindingContext is Recipes selectedRecipe)
            {
                await DisplayAlert("Recipe Details", $"Title: {selectedRecipe.Title}\nDescription: {selectedRecipe.Description}\nIngredients: {selectedRecipe.Ingredients}\nInstructions: {selectedRecipe.Instructions}", "OK");

            }
            else
            {
                // Handle case where the selected item is not of type Recipes
                await DisplayAlert("Error", "Invalid item selected", "OK");
            }
        }


    }
}