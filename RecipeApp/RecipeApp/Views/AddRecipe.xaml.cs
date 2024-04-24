using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using RecipeApp.Models;

namespace RecipeApp.Views
{
    public partial class AddRecipe : ContentPage
    {
        private readonly HttpClient _httpClient;

        public AddRecipe()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnGetAllRecipesClicked(object sender, EventArgs e)
        {
            try
            {
                var response = await _httpClient.GetAsync("https://recipeapp97.azurewebsites.net/recipe");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var recipes = JsonConvert.DeserializeObject<List<Recipes>>(content);

                    // Format recipes into a string
                    var recipesString = "";
                    foreach (var recipe in recipes)
                    {
                        recipesString += $"Recipe ID: {recipe.RecipeId}\n" +
                                         $"Title: {recipe.Title}\n" +
                                         $"Description: {recipe.Description}\n" +
                                         $"Ingredients: {recipe.Ingredients}\n" +
                                         $"Instructions: {recipe.Instructions}\n" +
                                         $"Category: {recipe.Category}\n" +
                                         $"Image URL: {recipe.ImageUrl}\n\n";
                    }

                    // Display the formatted recipes in a DisplayAlert
                    await DisplayAlert("Recipes", recipesString, "OK");
                }
                else
                {
                    await DisplayAlert("Error", $"Failed to get recipes. Status code: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
