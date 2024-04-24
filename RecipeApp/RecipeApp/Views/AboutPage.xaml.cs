using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using RecipeApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace RecipeApp.Views
{
    public partial class AboutPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public AboutPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }


        private async void OnCategoryButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string category = button.Text;

                // Call the method to get recipes by category
                var recipes = await GetRecipesByCategoryAsync(category);

                // Do something with the recipes
                if (recipes != null && recipes.Any())
                {
                    // Bind the recipes to the CollectionView
                    recipesCollectionView.ItemsSource = recipes;

                    // Show the data in a DisplayAlert (optional)
                    string recipesString = string.Join(Environment.NewLine, recipes.Select(r => $"{r.Title}: {r.Description}"));
                    await DisplayAlert("Recipes", recipesString, "OK");
                }
                else
                {
                    // Handle case where no recipes are returned
                    await DisplayAlert("No Recipes", "No recipes found for this category.", "OK");
                }
            }
        }

        private async void OnFetchDataClicked(object sender, EventArgs e)
        {
            string category = "Breakfast"; // Set the category to "breakfast"

            // Call the method to fetch data by category
            var recipes = await GetRecipesByCategoryAsync(category);

            // Do something with the fetched recipes
            if (recipes != null && recipes.Any())
            {
                // Show the data in a DisplayAlert (optional)
                string recipesString = string.Join(Environment.NewLine, recipes.Select(r => $"{r.Title}: {r.Description}"));
                await DisplayAlert("Recipes", recipesString, "OK");
            }
            else
            {
                // Handle case where no recipes are returned
                await DisplayAlert("No Recipes", "No recipes found for this category.", "OK");
            }
        }

        private async Task<List<Recipes>> GetRecipesByCategoryAsync(string category)
        {
            try
            {
                string apiUrl = $"https://recipeapp97.azurewebsites.net/recipe/category/{category}";
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var recipes = JsonConvert.DeserializeObject<List<Recipes>>(content);
                    return recipes;
                }
                else
                {
                    // Handle unsuccessful response
                    Console.WriteLine($"Failed to get recipes. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
