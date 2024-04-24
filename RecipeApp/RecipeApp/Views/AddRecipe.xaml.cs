using Newtonsoft.Json;
using RecipeApp.Models;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace RecipeApp.Views
{
    public partial class AddRecipe : ContentPage
    {
        private string selectedCategory;
        private readonly HttpClient _httpClient;

        public AddRecipe()
        {
            InitializeComponent();

            // Disable the "Add Recipe" button initially
            AddRecipeButton.IsEnabled = false;
            _httpClient = new HttpClient();

            // Set the default selected category to "Breakfast"
            selectedCategory = "Breakfast";
            // Highlight the "Breakfast" button visually
            BreakfastButton.BackgroundColor = Color.LightBlue;

        }

   private void OnTextChanged(object sender, EventArgs e)
{
    // Check if all fields are filled to enable the "Add Recipe" button
    bool isFieldsFilled = true;

    // Create an array of all the entry and editor fields
    var fields = new View[] { TitleEntry, DescriptionEditor, IngredientsEditor, InstructionsEditor, ImageUrlEntry };

    // Iterate through each field
    foreach (var field in fields)
    {
        // Check if the field is empty
        if (field is Entry entry && string.IsNullOrWhiteSpace(entry.Text))
        {
            isFieldsFilled = false; // If any field is empty, set the flag to false
            break; // Exit the loop early
        }
        else if (field is Editor editor && string.IsNullOrWhiteSpace(editor.Text))
        {
            isFieldsFilled = false; // If any field is empty, set the flag to false
            break; // Exit the loop early
        }
    }

    // Enable/disable the "Add Recipe" button based on the filled fields
    AddRecipeButton.IsEnabled = isFieldsFilled;
}


        private void OnCategoryButtonClicked(object sender, EventArgs e)
        {
            // Reset the background color of all category buttons
            ResetCategoryButtonColors();

            // Get the selected category from the clicked button
            Button selectedButton = (Button)sender;
            selectedCategory = selectedButton.Text;

            // Highlight the selected button visually
            selectedButton.BackgroundColor = Color.LightBlue;
        }

        private async void OnAddRecipeClicked(object sender, EventArgs e)
        {
            try
            {
                // Create a new Recipes object with the data from the input fields
                var newRecipe = new Recipes
                {
                    Title = TitleEntry.Text,
                    Description = DescriptionEditor.Text,
                    Ingredients = IngredientsEditor.Text,
                    Instructions = InstructionsEditor.Text,
                    ImageUrl = ImageUrlEntry.Text,
                    Category = selectedCategory
                };

                // Serialize the new recipe object to JSON
                var json = JsonConvert.SerializeObject(newRecipe);

                // Create HttpContent for the request
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Define the API endpoint URL
                var apiUrl = "https://recipeapp97.azurewebsites.net/recipe/";

                // Send a POST request to add the new recipe
                var response = await _httpClient.PostAsync(apiUrl, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message
                    await DisplayAlert("Success", "Recipe added successfully.", "OK");
                }
                else
                {
                    // Display an error message if the request failed
                    await DisplayAlert("Error", "Failed to add recipe.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        // Helper method to reset the background color of all category buttons
        private void ResetCategoryButtonColors()
        {
            BreakfastButton.BackgroundColor = Color.Default;
            LunchButton.BackgroundColor = Color.Default;
            DinnerButton.BackgroundColor = Color.Default;
            AppetizerButton.BackgroundColor = Color.Default;
            DessertsButton.BackgroundColor = Color.Default;
        }
    }
}
