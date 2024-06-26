﻿using System;
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
        public readonly HttpClient _httpClient;

        public AboutPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }


        public async void OnCategoryButtonClicked(object sender, EventArgs e)
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
        public async void OnImageTapped(object sender, EventArgs e)
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




        public async Task<List<Recipes>> GetRecipesByCategoryAsync(string category)
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
